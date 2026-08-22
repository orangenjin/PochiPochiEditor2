using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class DefReader
    {
        // パス用
        private string _defFolder =
            Path.Combine(Application.StartupPath, Constants.DefExt);

        // 公開用
        public List<FieldMetaData> FieldDefs { get; }

        public DefReader(string fileName)
        {
            var splitLines = ReadFile(fileName);
            FieldDefs = ReadFields(splitLines);
        }

        /// <summary>
        /// ファイルから行を読み込み、要素をコロンで分割する。
        /// </summary>
        private List<string[]> ReadFile(string fileName)
        {
            // 戻り値用
            List<string[]> splitLines = new List<string[]>();

            // defフォルダ階層下から指定したファイルを探す
            var foundFiles = Directory.GetFiles(
                _defFolder,
                Path.ChangeExtension(fileName, Constants.DefExt), 
                SearchOption.AllDirectories);

            // 最初にヒットしたもの
            var allLines = File.ReadAllLines(foundFiles[Constants.DefaultIndex]);

            foreach (var line in allLines)
            {
                // 空行をスキップ
                if (string.IsNullOrWhiteSpace(line)) continue;

                // コロン分割
                var parts = line
                    .Split(Constants.ColonChar)
                    .Select(p => p.Trim())
                    .ToArray();

                splitLines.Add(parts);
            }

            return splitLines;
        }

        /// <summary>
        /// 行ごとに定義を読み取る。
        /// </summary>
        private List<FieldMetaData> ReadFields(List<string[]> splitLines)
        {
            // 戻り値用
            var fieldDefs = new List<FieldMetaData>();

            foreach (var line in splitLines)
            {
                // フィールド名を読み取る
                string name = line[(int)FieldExtensions.DefName.FieldName];

                // 型を読み取る
                var kindStr = line[(int)FieldExtensions.DefName.KindName];
                if (!Enum.TryParse<FieldExtensions.FieldKind>(kindStr, true, out var kind)) continue;

                // 属性読み取る
                var attrs = new List<FieldAttribute>();
                for (int i = (int)FieldExtensions.DefName.AttrName; i < line.Length; i++)
                {
                    // 解析対象を取り出す
                    var attrText = line[i];

                    // "("の位置を探す
                    var openParenIndex = attrText.IndexOf(Constants.OpenParenChar);

                    // 属性名のみ
                    if (openParenIndex == Constants.InvalidValue)
                    {
                        // 属性名を取得
                        var attrName = (FieldExtensions.AttrKind)Enum.Parse(
                            typeof(FieldExtensions.AttrKind),
                            attrText);

                        // 属性引数なし（要素数0を格納）
                        attrs.Add(new FieldAttribute(attrName, Array.Empty<string>()));

                        continue;
                    }

                    //　")"の位置を探す
                    var closeParenIndex = attrText.LastIndexOf(Constants.CloseParenChar);

                    // 属性名を取得
                    var attrStr = attrText.Substring(
                        Constants.DefaultIndex,
                        openParenIndex);
                    var attrKind = (FieldExtensions.AttrKind)Enum.Parse(
                        typeof(FieldExtensions.AttrKind),
                        attrStr);

                    // 属性引数を取得
                    var rawArgs = line[i].Substring(
                        openParenIndex + 1, 
                        closeParenIndex - openParenIndex - 1);
                    // カンマで分割
                    var argParts = rawArgs
                        .Split(Constants.CommaChar)
                        .Select(p => p.Trim())
                        .ToArray();

                    // 属性名と引数を格納
                    attrs.Add(new FieldAttribute(attrKind, argParts));
                }

                // フィールド定義を格納
                fieldDefs.Add(new FieldMetaData(name, kind, attrs));
            }

            return fieldDefs;
        }
    }
}
