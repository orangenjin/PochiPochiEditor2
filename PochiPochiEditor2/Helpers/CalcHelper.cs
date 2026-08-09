using System.Globalization;

namespace PochiPochiEditor2.Helpers
{
    public static class CalcHelper
    {
        /// <summary>
        /// stringからintへ16進数を変換する。
        /// </summary>
        public static bool TryParseValue(string offsetStr, out int offsetValue)
        {
            return int.TryParse(offsetStr, NumberStyles.HexNumber, null, out offsetValue);
        }
    }
}
