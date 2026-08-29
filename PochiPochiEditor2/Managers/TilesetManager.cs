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
        // 共有データ用
        private SharedData _sharedData = null;

        // タイルセット番号管理するため
        public int BaseOffset { get; set; }
        public int EntryLength { get; set; }

        private enum FieldKey
        {
            ImageCompType,
            PaletteType,
            TilesetHeaderUnk1,
            TilesetHeaderUnk2,
            ImageOffset,
            PaletteOffset,
            BlockArg1Offset,
            AnimDataOffset,
            BlockArg2Offset
        }

        private static class DefName
        {
            public static string TilesetHeaderEntry = nameof(TilesetHeaderEntry);
        }

        private static class IniKey
        {
            public static string TilesetHeaderBaseOffset = nameof(TilesetHeaderBaseOffset);
        }

        public TilesetManager(SharedData sharedData)
        {
            _sharedData = sharedData;
            BaseOffset = _sharedData.Config.ReadInt(IniKey.TilesetHeaderBaseOffset);

            // エントリーサイズを求める
            var tilesetHeaderDef = new DefReader(DefName.TilesetHeaderEntry);
            var entryFields = new List<FieldValue>();
            for (int i = 0; i < tilesetHeaderDef.FieldDefs.Count; i++)
            {
                // FieldValueを生成
                var fieldValue = new FieldValue(
                    _sharedData,
                    tilesetHeaderDef.FieldDefs[i],
                    typeof(FieldKey));

                entryFields.Add(fieldValue);
            }
            EntryLength = entryFields.Sum(f => f.EntryLength);
        }

        /// <summary>
        /// タイルセット番号からヘッダーオフセットを計算する。
        /// </summary>
        public int CalcOffset(int tilesetNo)
        {
            return BaseOffset + (tilesetNo * EntryLength);
        }

        /// <summary>
        /// ヘッダーオフセットからタイルセット番号を計算する。
        /// 完全一致しない場合は失敗する。
        /// </summary>
        public bool TryCalcTilesetNo(int offset, out int tilesetNo)
        {
            tilesetNo = Constants.InvalidValue;

            if (offset < BaseOffset) return false;

            int diff = offset - BaseOffset;
            if (diff % EntryLength != 0) return false;

            tilesetNo = diff / EntryLength;
            return true;
        }

        /// <summary>
        /// 指定されたオフセットに近いタイルセット番号を取得する。
        /// </summary>
        public int CalcNearestTilesetNo(int offset)
        {
            int diff = offset - BaseOffset;
            if (diff < 0) return Constants.DefaultIndex;

            return (diff / EntryLength) + 1;
        }
    }
}
