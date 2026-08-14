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
        private string _defFolder = Path.Combine(Application.StartupPath, Constants.DefExt);

        // 公開用
        public List<FieldMetaData> FieldDefs { get; }
        public List<ControlKind> CtrlDefs { get; }

        public DefReader(string fileName)
        {
            var splitLines = ReadFile(fileName);
            FieldDefs = ReadFields(splitLines);
            CtrlDefs = ReadCtrls(splitLines);
        }

        /// <summary>
        /// ファイルから行を読み込み、コロンで分割など。
        /// </summary>
        private List<string[]> ReadFile(string fileName)
        {
            // 戻り値用
            List<string[]> splitLines = new List<string[]>();

            // defフォルダ階層下から指定したファイルを探す
            var foundFiles = Directory.GetFiles(_defFolder, fileName + "." + Constants.DefExt, SearchOption.AllDirectories);
            var allLines = File.ReadAllLines(foundFiles[0]);

            foreach (var line in allLines)
            {
                var trimmedLine = line.Trim();

                // 空行をスキップ
                if (string.IsNullOrWhiteSpace(trimmedLine)) continue;

                // コロン分割
                var parts = trimmedLine.Split(Constants.ColonChar).Select(p => p.Trim()).ToArray();
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
                // 型を読み取る
                var kindStr = line[(int)DefPosition.KindName];
                if (!Enum.TryParse<FieldKind>(kindStr, true, out var kind)) continue;

                // 属性読み取る
                var attributes = new List<FieldAttribute>();
                for (int i = (int)DefPosition.AttributeName; i < line.Length; i++)
                {
                    // 角括弧を外す
                    var target = line[i].Trim(Constants.OpenBracketChar, Constants.CloseBracketChar);
                    // 丸括弧の位置を探す
                    var openParenIndex = target.IndexOf(Constants.OpenParenChar);
                    var closeParenIndex = target.LastIndexOf(Constants.CloseParenChar);

                    // 属性名を取得
                    var attributeName = target.Substring(0, openParenIndex);

                    // パラメータを取得
                    var rawParams = target.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
                    // カンマで分割
                    var paramParts = rawParams.Split(Constants.CommaChar).Select(p => p.Trim()).ToArray();

                    // パラメータを格納
                    if (Enum.TryParse<AttributeKind>(attributeName, true, out var attrType))
                    {
                        var paramList = paramParts
                            .Select(p => p.Trim().Trim(Constants.QuotationChar))
                            .ToArray();

                        attributes.Add(new FieldAttribute(attrType, paramList));
                    }
                }

                // フィールド定義を格納
                fieldDefs.Add(new FieldMetaData(line[(int)DefPosition.FieldName], kind, attributes));
            }

            return fieldDefs;
        }

        private List<ControlKind> ReadCtrls(List<string[]> splitLines)
        {
            // 戻り値用
            var ctrlDefs = new List<ControlKind>();

            foreach (var line in splitLines)
            {
                // コントロール定義を読み取る
                var ctrlStr = line[(int)DefPosition.CtrlName];
                if (!Enum.TryParse<ControlKind>(ctrlStr, true, out var ctrl)) continue;

                ctrlDefs.Add(ctrl);
            }

            return ctrlDefs;
        }
    }

    /// <summary>
    /// バインドできるコントロールの定義をする。
    /// </summary>
    public enum ControlKind
    {
        txt,
        nud,
        cmb,
        chk,
        rb
    }

    /// <summary>
    /// コロンで区切られた順番。
    /// </summary>
    public enum DefPosition
    {
        FieldName,
        KindName,
        CtrlName,
        AttributeName
    }
}
