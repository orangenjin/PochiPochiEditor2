using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldReader
    {
        private string _defFolder = Path.Combine(Application.StartupPath, "def");

        // 公開用
        public List<FieldMetadata> Fields { get; }

        public FieldReader(string fileName)
        {
            Fields = ReadFields(fileName);
        }

        private List<FieldMetadata> ReadFields(string fileName)
        {
            // 戻り値用
            var results = new List<FieldMetadata>();

            // defフォルダ階層下から指定したファイルを探す
            var foundFiles = Directory.GetFiles(_defFolder, fileName, SearchOption.AllDirectories);
            var filePath = foundFiles[0];
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                // 空行をスキップ
                if (string.IsNullOrWhiteSpace(trimmedLine)) continue;

                // コロンで分割
                var parts = line.Split(Constants.ColonChar).Select(p => p.Trim()).ToArray();

                // 型を読み取る
                var kindStr = parts[1];
                if (!Enum.TryParse<FieldKind>(kindStr, true, out var kind)) continue;

                // 属性読み取る
                var attributes = new List<FieldAttribute>();
                for (int i = 2; i < parts.Length; i++)
                {
                    // 角括弧を外す
                    var target = parts[i].Trim(Constants.OpenBracketChar, Constants.CloseBracketChar);
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
                    if (Enum.TryParse<AttributeType>(attributeName, true, out var attrType))
                    {
                        var paramList = paramParts
                            .Select(p => p.Trim().Trim(Constants.QuotationChar))
                            .ToArray();

                        attributes.Add(new FieldAttribute(attrType, paramList));
                    }
                }

                // 単一フィールド定義を格納
                results.Add(new FieldMetadata(parts[0], kind, attributes));
            }

            return results;
        }
    }
}
