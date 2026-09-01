using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Managers.Tilesets;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class TilesetManager
    {
        public TilesetHeader TilesetData {get; set;}

        // 共有データ用
        private SharedData _sharedData = null;
        // タイルセット番号管理するため
        private int _baseOffset = default;
        private int _entryLength = default;

        private static class IniKey
        {
            public static string TilesetHeaderBaseOffset = nameof(TilesetHeaderBaseOffset);
        }

        public TilesetManager(SharedData sharedData)
        {
            _sharedData = sharedData;
            TilesetData = new TilesetHeader(_sharedData);
            _baseOffset = _sharedData.Config.ReadInt(IniKey.TilesetHeaderBaseOffset);
            _entryLength = TilesetData.GetEntryLength();



        }

        public void ReadHeader(int tilesetNo)
        {
            // 初期化
            TilesetData.Clear();
            // 読み込み
            TilesetData.Create(CalcOffset(tilesetNo));

        }



















        /// <summary>
        /// タイルセット番号からヘッダーオフセットを計算する。
        /// </summary>
        public int CalcOffset(int tilesetNo)
        {
            return _baseOffset + (tilesetNo * _entryLength);
        }

        /// <summary>
        /// ヘッダーオフセットからタイルセット番号を計算する。
        /// 完全一致しない場合は失敗する。
        /// </summary>
        public bool TryCalcTilesetNo(int offset, out int tilesetNo)
        {
            tilesetNo = Constants.InvalidValue;

            if (offset < _baseOffset) return false;

            int diff = offset - _baseOffset;
            if (diff % _entryLength != 0) return false;

            tilesetNo = diff / _entryLength;
            return true;
        }

        /// <summary>
        /// 指定されたオフセットに近いタイルセット番号を取得する。
        /// </summary>
        public int CalcNearestTilesetNo(int offset)
        {
            int diff = offset - _baseOffset;
            if (diff < 0) return Constants.DefaultIndex;

            return (diff / _entryLength) + 1;
        }
    }
}
