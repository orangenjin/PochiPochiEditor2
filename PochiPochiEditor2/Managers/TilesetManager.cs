using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class TilesetManager
    {
        public Entry HeaderEntry { get; set; }
        public byte[] ImageData { get; set; }
        public List<byte[]> PaletteData { get; set; }
        public List<Entry> BlockEntry { get; set; }

        // コンストラクタで事前に計算
        public List<FieldValue> _headerFields = null;
        public List<FieldValue> _blockFields = null;
        public List<FieldValue> _attrFields = null;
        // 定数用
        private int _baseHeaderOffset = default;
        private int _headerEntryLength = default;
        private int _blockDataEntryLength = default;
        private int _blockAttrEntryLength = default;
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

        public enum FieldKey
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

        private static class IniKey
        {
            public static string TilesetHeaderBaseOffset = nameof(TilesetHeaderBaseOffset);
        }

        private static class DefName
        {
            public static string TilesetHeaderEntry = nameof(TilesetHeaderEntry);
            public static string BlockDataEntry = nameof(BlockDataEntry);
            public static string AttrDataEntry = nameof(AttrDataEntry);
        }

        public TilesetManager(SharedData sharedData)
        {
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
                        sharedData,
                        def.FieldDefs[i],
                        typeof(FieldKey));

                    fields.Add(fieldValue);
                }
                return fields;
            }

            // 定数を計算
            _baseHeaderOffset = sharedData.Config.ReadInt(IniKey.TilesetHeaderBaseOffset);
            _headerEntryLength = _headerFields.Sum(f => f.EntryLength);
            _blockDataEntryLength = _blockFields.Sum(f => f.EntryLength);
            _blockAttrEntryLength = _attrFields.Sum(f => f.EntryLength);
        }

        public void ReadHeader(int tilesetNo, SharedData sharedData)
        {
            var offset = CalcOffset(tilesetNo);
            HeaderEntry = new Entry(offset, Constants.DefaultIndex, _headerFields);

            // 画像データとパレットデータ
            ImageData = LoadImage(sharedData);
            PaletteData = LoadPalettes(sharedData);

            // ブロックデータ
            var blockDataTableOffset = HeaderEntry[FieldKey.BlockDataTableOffset].GetData<int>();
            var blockCount = CalcBlockCount();
            ReadBlockData(blockDataTableOffset, blockCount);




        }

        private byte[] LoadImage(SharedData sharedData)
        {
            try
            {
                var imageOffset = HeaderEntry[FieldKey.ImageOffset].GetData<int>();

                // 圧縮の場合
                if (Convert.ToBoolean(HeaderEntry[FieldKey.ImageCompType].GetData<int>()))
                {
                    byte[] decompressed = ImageHelper.DecompressLZ77(
                        sharedData.RomData,
                        imageOffset);

                    return decompressed ?? Array.Empty<byte>();
                }

                // 非圧縮の場合
                var maxHeight = (HeaderEntry[FieldKey.PaletteType].GetData<int>() == (int)PaletteKind.Pal0to6)
                    ? Tileset1ImageHeight
                    : Tileset2ImageMaxHeight;
                var maxSize = (TilesetImageWidth * maxHeight) / Constants.PixelsPerByte4Bpp;
                var imageData = new byte[maxSize];
                Array.Copy(sharedData.RomData, imageOffset, imageData, Constants.DefaultIndex, maxSize);

                return imageData;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        private List<byte[]> LoadPalettes(SharedData sharedData)
        {
            // 戻り値用
            var paletteDataList = new List<byte[]>();
            // 計算用
            var paletteDataLength = Constants.PalColorCount * Constants.BytesPerColor;

            try
            {
                var basePaletteOffset = HeaderEntry[FieldKey.PaletteOffset].GetData<int>();
                for (int i = 0; i < PaletteCount - 1; i++)
                {
                    var currentPos = basePaletteOffset + i * paletteDataLength;
                    var paletteData = ImageHelper.DecompressPalette(
                        sharedData.RomData,
                        currentPos,
                        isCompressed: false);
                    paletteDataList.Add(paletteData ?? Array.Empty<byte>());
                }
                return paletteDataList;
            }
            catch
            {
                return new List<byte[]>();
            }
        }

        public void ReadBlockData(int baseOffset, int blockCount)
        {
            BlockEntry = new List<Entry>();
            for (int i = 0; i < blockCount; i++)
            {
                BlockEntry.Add(new Entry(baseOffset, i, _blockFields));
            }
        }

        private int CalcBlockCount()
        {
            if (HeaderEntry[FieldKey.PaletteType].GetData<int>() == (int)PaletteKind.Pal0to6)
            {
                return Tileset1BlockAmount;
            }
            else
            {
                var blockDataTableOffset = HeaderEntry[FieldKey.BlockDataTableOffset].GetData<int>();
                var blockAttrTableOffset = HeaderEntry[FieldKey.BlockAttrTableOffset].GetData<int>();
                var expectedCount = (blockAttrTableOffset - blockDataTableOffset) / _blockDataEntryLength;
                return Math.Min(expectedCount, Tileset2BlockMaxAmount);
            }
        }










        /// <summary>
        /// タイルセット番号からヘッダーオフセットを計算する。
        /// </summary>
        public int CalcOffset(int tilesetNo)
        {
            return _baseHeaderOffset + (tilesetNo * _headerEntryLength);
        }

        /// <summary>
        /// ヘッダーオフセットからタイルセット番号を計算する。
        /// 完全一致しない場合は失敗する。
        /// </summary>
        public bool TryCalcTilesetNo(int offset, out int tilesetNo)
        {
            tilesetNo = Constants.InvalidValue;

            if (offset < _baseHeaderOffset) return false;

            int diff = offset - _baseHeaderOffset;
            if (diff % _headerEntryLength != 0) return false;

            tilesetNo = diff / _headerEntryLength;
            return true;
        }

        /// <summary>
        /// 指定されたオフセットに近いタイルセット番号を取得する。
        /// </summary>
        public int CalcNearestTilesetNo(int offset)
        {
            int diff = offset - _baseHeaderOffset;
            if (diff < 0) return Constants.DefaultIndex;

            return (diff / _headerEntryLength) + 1;
        }
    }
}
