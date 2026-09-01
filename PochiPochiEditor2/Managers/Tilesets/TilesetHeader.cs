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
    public class TilesetHeader
    {
        public int ImageCompType
        {
            get => _headerEntry[FieldKey.ImageCompType].GetData<int>();
            set { }
        }
        public int ImageOffset
        {
            get => _headerEntry[FieldKey.ImageOffset].GetData<int>();
            set { }
        }
        public byte[] ImageData { get; set; }
        public PaletteKind PaletteType
        {
            get => (PaletteKind)_headerEntry[FieldKey.PaletteType].GetData<int>();
            set { }
        }
        public int PaletteOffset
        {
            get => _headerEntry[FieldKey.PaletteOffset].GetData<int>();
            set { }
        }
        public List<byte[]> PaletteData { get; set; }
        public int BlockDataTableOffset
        {
            get => _headerEntry[FieldKey.BlockDataTableOffset].GetData<int>();
            set { }
        }
        public List<BlockData> Blocks { get; set; }
        public int BlockCount { get; set; }
        public int AnimHeaderOffset
        {
            get => _headerEntry[FieldKey.AnimHeaderOffset].GetData<int>();
            set { }
        }
        public List<AnimData> Anims { get; set; }
        public int BlockAttrTableOffset
        {
            get => _headerEntry[FieldKey.BlockAttrTableOffset].GetData<int>();
            set { }
        }
        public List<BlockAttrData> Attrs { get; set; }

        // 共有データ用
        private SharedData _sharedData = null;
        // コンストラクタで事前に計算
        private List<FieldValue> _headerFields = null;
        private List<FieldValue> _blockFields = null;
        private List<FieldValue> _attrFields = null;
        // 読み書き用
        private Entry _headerEntry = null;

        // 計算用
        private const int TilesetImageWidth = 128;
        private const int Tileset1ImageHeight = 320;
        private const int Tileset2ImageMaxHeight = 192;
        private const int Tileset1BlockAmount = Tileset1ImageHeight * Constants.PixelsPerByte4Bpp;
        private const int Tileset2BlockMaxAmount = Tileset2ImageMaxHeight * Constants.PixelsPerByte4Bpp;
        private const int PaletteCount = 16;

        public enum PaletteKind
        {
            Pal0to6,
            Pal7to12
        }

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
            BlockAttrTableOffset,

            LowerTopLeft,
            LowerTopRight,
            LowerBottomLeft,
            LowerBottomRight,
            UpperTopLeft,
            UpperTopRight,
            UpperBottomLeft,
            UpperBottomRight,

            ActionAttr,
            TypeAttr,
            UnkAttr,
            LayerAttr
        }

        private static class DefName
        {
            public static string TilesetHeaderEntry = nameof(TilesetHeaderEntry);
            public static string BlockDataEntry = nameof(BlockDataEntry);
            public static string AttrDataEntry = nameof(AttrDataEntry);
        }

        public TilesetHeader(SharedData shareData)
        {
            // 共有データを保持
            _sharedData = shareData;

            // ヘッダー定義を読み込み
            _headerFields = GenerateContainer(new DefReader(DefName.TilesetHeaderEntry));
            // ブロック定義を読み込み
            _blockFields = GenerateContainer(new DefReader(DefName.BlockDataEntry));
            // 属性定義を読み込み
            _attrFields = GenerateContainer(new DefReader(DefName.AttrDataEntry));

            // ヘルパーメソッド
            List<FieldValue> GenerateContainer(DefReader def)
            {
                var fields = new List<FieldValue>();
                for (int i = 0; i < def.FieldDefs.Count; i++)
                {
                    var fieldValue = new FieldValue(
                        _sharedData,
                        def.FieldDefs[i],
                        typeof(FieldKey));

                    fields.Add(fieldValue);
                }
                return fields;
            }
        }

        public void Create(int headerOffset)
        {
            // 単一エントリーとして作成
            _headerEntry = new Entry(
                headerOffset,
                Constants.DefaultIndex,
                _headerFields);

            // まずパレットタイプからブロック数を特定
            if (PaletteType == PaletteKind.Pal0to6)
            {
                BlockCount = Tileset1BlockAmount;
            }
            else
            {
                var expectedCount = (BlockAttrTableOffset - BlockDataTableOffset) / _blockFields.Sum(f => f.EntryLength);
                BlockCount = Math.Min(expectedCount, Tileset2BlockMaxAmount);
            }

            // 画像データ読み込み
            ImageData = LoadImage();
            // パレットデータ読み込み
            PaletteData = LoadPalettes();
            // ブロックデータ読み込み

            // ブロック属性読み込み

            // アニメデータ読み込み（別途）

        }

        private byte[] LoadImage()
        {
            // 戻り値用
            byte[] imageData;

            // 圧縮形式に応じて格納
            if (Convert.ToBoolean(ImageCompType))
            {
                imageData = ImageHelper.DecompressLZ77(_sharedData.RomData, ImageOffset);
            }
            else
            {
                var maxheight = (PaletteType == PaletteKind.Pal0to6)
                    ? Tileset1ImageHeight
                    : Tileset2ImageMaxHeight;

                var maxSize = (TilesetImageWidth * maxheight) / Constants.PixelsPerByte4Bpp;
                imageData = new byte[maxSize];
                Array.Copy(_sharedData.RomData, ImageOffset, imageData, Constants.DefaultIndex, maxSize);
            }

            return imageData;
        }

        private List<byte[]> LoadPalettes()
        {
            // 戻り値用
            var paletteDataList = new List<byte[]>();

            // 計算用
            var paletteDataLength = Constants.PalColorCount * Constants.BytesPerColor;

            for (int i = 0; i < PaletteCount - 1; i++)
            {
                var currentPos = PaletteOffset + i * paletteDataLength;

                var paletteData = ImageHelper.DecompressPalette(
                    _sharedData.RomData, 
                    currentPos,
                    isCompressed: false);
                paletteDataList.Add(paletteData);
            }

            return paletteDataList;
        }













        /// <summary>
        /// Create実行前に実行する。
        /// </summary>
        public void Clear()
        {
            ImageData = null;
            PaletteData = null;
            Blocks = null;
            BlockCount = default;
            Attrs = null;
            Anims = null;
        }

        public int GetEntryLength() => _headerFields.Sum(f => f.EntryLength);
    }
}
