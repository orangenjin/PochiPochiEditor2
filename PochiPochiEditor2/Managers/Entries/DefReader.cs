using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PochiPochiEditor2.Managers.Entries
{
    public class DefReader
    {
        private string _defFolder = Path.Combine(Application.StartupPath, "def");

        public DefReader(string fileName)
        {
            // defフォルダ階層下から指定したファイルを探す
            var foundFiles = Directory.GetFiles(_defFolder, fileName, SearchOption.AllDirectories);
            var filePath = foundFiles[0];
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                // 空白やコメントをスキップ
                if (string.IsNullOrWhiteSpace(trimmedLine) || 
                    trimmedLine.StartsWith(Constants.CommentChar.ToString()))
                    continue;

                // コロンで分割
                var parts = line.Split(Constants.ColonChar).Select(p => p.Trim()).ToArray();

                // 型を読み取る
                var kindStr = parts[1];
                if (!Enum.TryParse<FieldKind>(kindStr, true, out var kind)) continue;

                // まずクラス名と型を格納
                var metadata = new FieldMetadata(parts[0], kind);

                // 属性読み取り開始
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

                    if (Enum.TryParse<AttributeType>(attributeName, true, out var attrType))
                    {
                        var paramList = new List<string>();

                        foreach (var param in paramParts)
                        {
                            var cleaned = param.Trim().Trim(Constants.QuotationChar);
                            paramList.Add(cleaned);
                        }

                        metadata.Attributes.Add(new FieldAttribute(attrType, paramList.ToArray()));
                    }
                }
            }
        }
    }
}
