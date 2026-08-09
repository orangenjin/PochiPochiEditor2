using System.Globalization;

namespace PochiPochiEditor2.Helpers
{
    public static class CalcHelper
    {
        /// <summary>
        /// stringからintへ16進数アドレスを変換する。
        /// </summary>
        public static bool TryParseOffset(string offsetStr, out int offsetValue)
        {
            return int.TryParse(offsetStr, NumberStyles.HexNumber, null, out offsetValue);
        }
    }
}
