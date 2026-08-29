using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Commands;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;
using PochiPochiEditor2.Utilities.Tokens;

namespace PochiPochiEditor2.Managers.Tilesets
{
    public class TilesetData
    {
        public byte[] Image { get; set; }
        public bool IsCompress { get; set; }
        public PaletteKind PaletteType { get; set; }
        public List<byte[]> Palettes { get; set; }
        public List<BlockData> Blocks { get; set; }
        public int BlockCount { get; set; }
        public List<BlockAttrData> Attrs { get; set; }
        public int AnimHeaderOffset { get; set; }

        public enum PaletteKind
        { 
            Pal0to6,
            Pal7to12
        }

        // 共有データ用
        private SharedData _sharedData = null;
        // コンストラクタで事前に計算
        private List<FieldValue> _headerFields = null;
        private List<FieldValue> _blockFields = null;
        private List<FieldValue> _attrFields = null;

        // 計算用
        private const int TilesetImageWidth = 128;
        private const int Tileset1ImageHeight = 320;
        private const int Tileset2ImageMaxHeight = 192;
        private const int Tileset1BlockAmount = Tileset1ImageHeight / Constants.PixelsPerByte4Bpp;
        private const int Tileset2BlockMaxAmount = Tileset2ImageMaxHeight / Constants.PixelsPerByte4Bpp;






        private enum FieldKey
        {
            ImageCompType,
            PaletteType,
            TilesetHeaderUnk1,
            TilesetHeaderUnk2,
            ImageOffset,
            PaletteOffset,
            BlockDataTableOffset,
            AnimHeaderOffset,
            BlockAttrTableOffset
        }

        private static class DefName
        {
            public static string TilesetHeaderEntry = nameof(TilesetHeaderEntry);
            public static string BlockDataEntry = nameof(BlockDataEntry);
            public static string AttrDataEntry = nameof(AttrDataEntry);
        }

        private static class IniKey
        {
            public static string TilesetHeaderBaseOffset = nameof(TilesetHeaderBaseOffset);
        }

        public TilesetData(SharedData shareData)
        {
            // 共有データを保持
            _sharedData = shareData;

            // ヘッダー定義を読み込み
            GenerateContainer(_headerFields, new DefReader(DefName.TilesetHeaderEntry));
            // ブロック定義を読み込み;
            GenerateContainer(_blockFields, new DefReader(DefName.BlockDataEntry));
            // 属性定義を読み込み
            GenerateContainer(_attrFields, new DefReader(DefName.AttrDataEntry));

            // ヘルパーメソッド
            void GenerateContainer(List<FieldValue> fields, DefReader def)
            {
                fields = new List<FieldValue>();
                for (int i = 0; i < def.FieldDefs.Count; i++)
                {
                    // FieldValueを生成
                    var fieldValue = new FieldValue(
                        _sharedData,
                        def.FieldDefs[i],
                        typeof(FieldKey));

                    fields.Add(fieldValue);
                }
            }
        }

        public TilesetData Create(int headerOffset)
        {
            // 単一エントリーとして作成
            var entry = new Entry(
                headerOffset,
                Constants.DefaultIndex,
                _headerFields);

            // まずパレットタイプからブロック数を特定
            PaletteType = (PaletteKind)entry[FieldKey.PaletteType].GetData<int>();
            if (PaletteType == PaletteKind.Pal0to6)
            {
                BlockCount = Tileset1BlockAmount;
            }
            else
            {
                var blockDataTableOffset = entry[FieldKey.BlockDataTableOffset].GetData<int>();
                var blockAttrTableOffset = entry[FieldKey.BlockAttrTableOffset].GetData<int>();
                BlockCount = (blockAttrTableOffset - blockDataTableOffset) / _blockFields.Sum(f => f.EntryLength);
            }

            // 画像読み込み
            LoadImage(entry);

            return default;
        }

        private void LoadImage(Entry entry)
        {
            var imagePointerOffset = entry[FieldKey.ImageOffset].GetData<int>();
            if (IoHelper.TryReadPtr(_sharedData.RomData, imagePointerOffset, out int imageOffset))
            {
                // 圧縮形式を判定
                IsCompress = Convert.ToBoolean(entry[FieldKey.ImageCompType].GetData<int>());

                // 圧縮形式に応じて格納
                if (IsCompress)
                {
                    Image = ImageHelper.DecompressLZ77(_sharedData.RomData, imageOffset);
                }
                else
                {
                    int maxheight;
                    if (PaletteType == PaletteKind.Pal0to6)
                    {
                        maxheight = Tileset1ImageHeight;
                    }
                    else
                    {
                        maxheight = Tileset2ImageMaxHeight;
                    }
                    var maxSize = (TilesetImageWidth * maxheight) / Constants.PixelsPerByte4Bpp;
                    Array.Copy(_sharedData.RomData, imageOffset, Image, Constants.DefaultIndex, maxSize);
                };
            }
        }
















        public void Clear()
        {
            Image = null;
            IsCompress = default;
            PaletteType = default;
            Palettes = null;
            Blocks = null;
            Attrs = null;
            AnimHeaderOffset = default;
        }

        public int GetEntryLength() => _headerFields.Sum(f => f.EntryLength);

    }

    public class BlockData
    {

    }

    public class BlockAttrData
    {

    }
}
