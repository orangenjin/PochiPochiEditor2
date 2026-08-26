using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PochiPochiEditor2.Utilities.Tokens;

namespace PochiPochiEditor2.Utilities
{
    public class PatternMatcher
    {
        /// <summary>
        /// シンプルにパターンマッチングを試みる。
        /// </summary>
        public bool TryMatch(
            List<TokenData> tokens, 
            byte[] data, 
            int offset)
        {
            // カーソル用
            int currentPos = offset;

            // Listの要素数に対して
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                // 占有するバイト数
                int length = token.GetLength();

                // 対象となるバイト配列を取得
                token.Value = new byte[length];
                Array.Copy(
                    data,
                    currentPos, 
                    token.Value, 
                    Constants.DefaultIndex, 
                    length);

                // トークン判定、メソッドを抜ける
                if (!token.IsMatch()) return false;

                // 次のトークンへ
                currentPos += length;
            }

            return true;
        }

        /// <summary>
        /// パターンマッチングが連続する個数を取得する。
        /// </summary>
        public int TryCount(
            List<TokenData> tokens,
            byte[] data,
            int offset)
        {
            // 単一パターンの長さを取得
            int patternLength = GetPatternLength(tokens);

            int count = 0;
            int currentPos = offset;

            while (currentPos + patternLength <= data.Length)
            {
                // falseが戻るまで続ける
                if (!TryMatch(tokens, data, currentPos)) break;

                count++;
                currentPos += patternLength;
            }

            return count;
        }

        /// <summary>
        /// TokenDataの長さを計算する。
        /// </summary>
        private int GetPatternLength(List<TokenData> tokens)
        {
            int length = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                length += tokens[i].GetLength();
            }

            return length;
        }
    }
}
