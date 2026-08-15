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
                $"{fileName}{Constants.DotChar}{Constants.DefExt}", 
                SearchOption.AllDirectories);
            var allLines = File.ReadAllLines(foundFiles[Constants.DefaultIndex]);

            foreach (var line in allLines)
            {
                // 念のため
                var trimmedLine = line.Trim();

                // 空行をスキップ
                if (string.IsNullOrWhiteSpace(trimmedLine)) continue;

                // コロン分割
                var parts = trimmedLine
                    .Split(Constants.ColonChar)
                    .Select(p => p.Trim())
                    .ToArray();

                splitLines.Add(parts);
            }

            return splitLines;
        }

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

                // コントロール定義を読み取る
                var ctrlStr = line[(int)FieldExtensions.DefName.CtrlName];
                if (!Enum.TryParse<FieldExtensions.CtrlKind>(ctrlStr, true, out var ctrl)) continue;

                // 属性読み取る（ない場合を考慮）
                var attrs = new List<FieldAttribute>();
                for (int i = (int)FieldExtensions.DefName.AttrName; i < line.Length; i++)
                {
                    // 角括弧を外す
                    var target = line[i].Trim(Constants.OpenBracketChar, Constants.CloseBracketChar);
                    // 丸括弧の位置を探す
                    var openParenIndex = target.IndexOf(Constants.OpenParenChar);
                    var closeParenIndex = target.LastIndexOf(Constants.CloseParenChar);

                    // 属性名を取得
                    var attrName = target.Substring(Constants.DefaultIndex, openParenIndex);

                    // 属性引数を取得
                    var rawArgs = target.Substring(
                        openParenIndex + 1, 
                        closeParenIndex - openParenIndex - 1);
                    // カンマで分割
                    var argParts = rawArgs
                        .Split(Constants.CommaChar)
                        .Select(p => p.Trim())
                        .ToArray();

                    // 属性引数を格納
                    if (Enum.TryParse<FieldExtensions.AttrKind>(attrName, true, out var attrKind))
                    {
                        var argList = argParts
                            .Select(p => p.Trim().Trim(Constants.QuotationChar))
                            .ToArray();

                        attrs.Add(new FieldAttribute(attrKind, argList));
                    }
                }

                // フィールド定義を格納
                fieldDefs.Add(new FieldMetaData(name, kind, ctrl, attrs));
            }

            return fieldDefs;
        }
    }
}
