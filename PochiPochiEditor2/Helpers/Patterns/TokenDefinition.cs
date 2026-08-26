using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Helpers.Patterns
{
    public enum TokenType
    {
        Exact,
        Pointer,
        Range,
        Wildcard
    }

    /// <summary>
    /// 特定の値を設定し、比較する。
    /// </summary>
    public class ExactToken : IToken
    {
        public int ExactValue { get; }
        public int Length { get; }

        public ExactToken(int exactValue)
        {
            ExactValue = exactValue;
            Length = Constants.ByteSize;
        }

        public bool IsValid(byte[] bytes)
        {
            // 1バイト分を抽出
            var hexValue = bytes[Constants.DefaultIndex];

            return ExactValue == hexValue;
        }
    }

    public class PointerToken : IToken
    {
        // 再帰的マッチングのため
        public int Offset { get; set; }
        public int Length { get; }

        public PointerToken()
        {
            Length = Constants.UIntSize;
        }

        public bool IsValid(byte[] bytes)
        {
            // ポインタとして読み取る
            var result = IoHelper.TryReadPtr(
                bytes, Constants.DefaultIndex, out int offset);

            // 結果を格納
            Offset = offset;

            return result;
        }

        public bool IsSus => Offset == Constants.InvalidValue;
    }

    /// <summary>
    /// Range型、最小値と最大値を設定する。
    /// </summary>
    public class RangeToken : IToken
    {
        public int Min { get; }
        public int Max { get; }
        public int Length { get; }

        public RangeToken(int min, int max, int length)
        {
            Min = min;
            Max = max;
            Length = length;
        }

        public bool IsValid(byte[] bytes)
        {
            // リトルエンディアンで読み取る
            var hexValue = IoHelper.ReadByteValue(
                bytes, Constants.DefaultIndex, Length);

            // 範囲内かチェック
            return (uint)Min <= hexValue && hexValue <= (uint)Max;
        }

    }

    /// <summary>
    /// 何でもtrueを返す。
    /// </summary>
    public class WildcardToken : IToken
    {
        public int Length { get; }

        public WildcardToken()
        {
            Length = Constants.UIntSize;
        }

        public bool IsValid(byte[] bytes) => true;
    }


    public interface IToken
    {
        int Length { get; }
        bool IsValid(byte[] bytes);
    }
}
