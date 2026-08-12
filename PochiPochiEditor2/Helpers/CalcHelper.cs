using System.Globalization;

using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

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

        /// <summary>
        /// byte[]を汎用的なTとして変換する。
        /// </summary>
        public static T BytesToModelConv<T>(SharedData sharedData, FieldValue fieldValue, int index)
        {
            int entryLength = fieldValue.EntryLength;
            byte[] data = fieldValue.BinaryData;
            int startIndex = 0;
            int nameCount = fieldValue.ControlNames.Length;
            bool isSigned = fieldValue.IsSigned;

            // 文字列
            if (fieldValue.AllowedLength > 0)
            {
                return (T)(object)sharedData.Charmap.BytesToString(data, startIndex, entryLength);
            }

            switch(entryLength)
            {
                // 1バイト
                case Constants.ByteSize:
                    byte rawByte = (byte)IoHelper.ReadByteValue(
                        data,
                        startIndex,
                        Constants.ByteSize);
                    switch (nameCount)
                    {
                        // ニブル
                        case Constants.CharPerByte:
                            int nibbleValue = index == 0 // high
                                ? (rawByte >> Constants.NibbleShift) & Constants.NibbleMask
                                : rawByte & Constants.NibbleMask;
                            return (T)(object)nibbleValue;

                        // ビット
                        case Constants.BitsPerByte:
                            int bitValue = (rawByte >> index) & 1;
                            return (T)(object)bitValue;

                        default:
                            return isSigned
                                ? (T)(object)(sbyte)rawByte
                                : (T)(object)rawByte;
                    }

                // 2バイト
                case Constants.UShortSize:
                    ushort rawUShort = (ushort)IoHelper.ReadByteValue(
                        data,
                        startIndex,
                        Constants.UShortSize);
                    return isSigned
                        ? (T)(object)(short)rawUShort
                        : (T)(object)rawUShort;

                // 4バイト
                case Constants.UIntSize when fieldValue.IsPointer:
                    uint rawUInt = IoHelper.ReadByteValue(
                        data,
                        startIndex,
                        Constants.UIntSize);

                    // ポインタ
                    if (fieldValue.IsPointer)
                    {
                        return rawUInt == 0
                            ? (T)(object)Constants.InvalidOffsetValue
                            : (T)(object)(rawUInt - Constants.BaseAddr);
                    }

                    return isSigned
                        ? (T)(object)(int)rawUInt
                        : (T)(object)rawUInt;

                // その他
                default:
                    return (T)(object)default;
            }
        }
    }
}
