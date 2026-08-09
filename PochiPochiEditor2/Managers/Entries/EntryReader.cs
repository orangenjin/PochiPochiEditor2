using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PochiPochiEditor2.Managers.Entries
{
    public class EntryReader
    {
        private string _defFolder = Path.Combine(Application.StartupPath, "def");

        public EntryReader(string fileName)
        {
            // defフォルダの1階層下のフォルダから指定したファイルを探す
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

                // カンマで分割
                var parts = line.Split(Constants.CommaChar).Select(p => p.Trim()).ToArray();

                // enumにパースして、格納
                var kindStr = parts[1];
                if (!Enum.TryParse<FieldKind>(kindStr, true, out var kind)) continue;
                var metadata = new FieldMetadata(parts[0], kind);

                // コンストラクタで属性のリストが初期化された後
                for (int i = 2; i < parts.Length; i++)
                {
                    var target = parts[i];

                    // 角括弧を消す
                    var trimmed = target.Trim(Constants.OpenBracketChar, Constants.CloseBracketChar);
                    // 丸括弧の位置を探す
                    var openParenIndex = trimmed.IndexOf(Constants.OpenParenChar);

                    // 属性名を取得
                    var attributeName = trimmed.Substring(0, openParenIndex).Trim();

                    // パラメータを取得
                    var closeParenIndex = trimmed.LastIndexOf(Constants.CloseParenChar);
                    var paramString = trimmed.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);

                    // パース処理
                    if (Enum.TryParse<AttributeType>(attributeName, true, out var attrType))
                    {
                        var paramList = new List<string>();
                        var rawParams = paramString.Split(Constants.CommaChar);

                        foreach (var p in rawParams)
                        {
                            var cleaned = p.Trim().Trim(Constants.QuotationChar);
                            paramList.Add(cleaned);
                        }

                        metadata.Attributes.Add(new FieldAttribute(attrType, paramList.ToArray()));
                    }
                }
            }
        }
    }
}
