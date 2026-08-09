namespace PochiPochiEditor2.Constants
{
    public static class CalcConstant
    {
        public const int HexBase = 16;
        public const int BitsPerByte = 8;
        public const int CharPerByte = 2;
        public const int NibbleShift = 4;
        public const int NibbleMask = 0xF;
        public const int ByteMask = 0xFF;
        public const int UShortMask = 0xFFFF;
        public const uint UIntMask = 0xFFFFFFFFU;
        public const int ByteSize = 1;
        public const int UShortSize = 2;
        public const int UIntSize = 4;
        public const uint BaseAddr = 0x8000000U;

        public const int InvalidOffset = -1;
        public const string InvalidString = "null";
        public const string HexPrefix = "0x";
    }
}
