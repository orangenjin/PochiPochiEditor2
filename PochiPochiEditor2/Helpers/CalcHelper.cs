using System.Globalization;

namespace PochiPochiEditor2.Helpers
{
    public static class CalcHelper
    {
        /// <summary>
        /// stringからintへ16進数を変換する。
        /// </summary>
        public static bool TryParseValue(string str, out int val)
        {
            return int.TryParse(str, NumberStyles.HexNumber, null, out val);
        }
    }
}
