using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class IniManager
    {
        // 設定名からパスを取得
        private Dictionary<string, string> _configs = new Dictionary<string, string>();

        // 現在の設定値を格納
        private Dictionary<string, int> _iniCacheInt = new Dictionary<string, int>();
        private Dictionary<string, bool> _iniCacheBool = new Dictionary<string, bool>();

        /// <summary>
        /// 設定ファイルから数値を読み取る。
        /// 読み取れない場合は、defaultValueを返す。
        /// </summary>
        public int ReadInt(string key, int defaultValue = 0) =>
            key != null && _iniCacheInt.TryGetValue(key, out int value)
                ? value
                : defaultValue;

        /// <summary>
        /// 設定ファイルから真偽値を読み取る。
        /// 読み取れない場合は、defaultValueを返す。
        /// </summary>
        public bool ReadBool(string key, bool defaultValue = false) =>
            key != null && _iniCacheBool.TryGetValue(key, out bool value)
                ? value
                : defaultValue;

        /// <summary>
        /// 設定ファイル名とパスを格納する。
        /// </summary>
        public IniManager(string folderPath, ComboBox targetCmb)
        {
            if (!Directory.Exists(folderPath)) return;

            foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.ini"))
            {
                string name = Path.GetFileNameWithoutExtension(filePath);

                // 辞書へ
                _configs[name] = filePath;
                // コンボボックスへ
                targetCmb.Items.Add(name);
            }

            // 初期選択
            if (targetCmb.Items.Count > 0)
            {
                targetCmb.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 選択された設定名の内容を解析し、格納する。
        /// </summary>
        public void LoadConfig(string configName, byte[] data)
        {
            // 初期化
            _iniCacheInt.Clear();
            _iniCacheBool.Clear();

            // ファイルパスを取得
            if (!_configs.TryGetValue(configName, out string filePath)) return;

            // 設定値を解析
            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                // 空行とコメントをスキップ
                if (!TryParseLine(line, out string key, out string rawString)) continue;

                // boolかどうか
                if (bool.TryParse(rawString, out bool boolValue))
                {
                    _iniCacheBool[key] = boolValue;
                    continue;
                }

                // ポインタかどうか
                if (rawString.StartsWith("*"))
                {
                    if (TryParseNumber(rawString.Substring(1), out int ptrOffset))
                    {
                        if (IoHelper.TryReadPtr(data, ptrOffset, out int resultOffset))
                        {
                            _iniCacheInt[key] = resultOffset;
                            continue;
                        }
                    }
                }

                // 数字かどうか
                if (TryParseNumber(rawString, out int numValue))
                {
                    _iniCacheInt[key] = numValue;
                }
            }
        }

        /// <summary>
        /// 空行を除外して、stringとして分割する。
        /// </summary>
        private bool TryParseLine(string line, out string key, out string rawString)
        {
            key = string.Empty;
            rawString = string.Empty;

            // 除外行チェック
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(Constants.CommentChar.ToString())) return false;

            // イコールで分割
            string[] parts = line.Split(Constants.EqualChar);

            key = parts[0].Trim();
            rawString = parts[1].Trim();
            return true;
        }

        /// <summary>
        /// 10進数か16進数（0x付き）を判定し、intに変換する。
        /// </summary>
        private bool TryParseNumber(string rawString, out int parsedValue)
        {
            if (rawString.StartsWith(Constants.HexPrefix)) // 0x
            {
                string hexPart = rawString.Substring(Constants.HexPrefix.Length);
                return CalcHelper.TryParseValue(hexPart, out parsedValue);
            }

            return int.TryParse(rawString, out parsedValue); // 10進数
        }
    }
}
