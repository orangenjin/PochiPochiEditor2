using System;
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
        public static T BytesToModelConv<T>(
            SharedData sharedData, 
            FieldValue fieldValue,
            int ctrlNameindex)
        {
            int entryLength = fieldValue.EntryLength;
            byte[] data = fieldValue.BinaryData;
            int startIndex = 0;
            int nameCount = fieldValue.ControlNames.Length;
            bool isSigned = fieldValue.IsSigned;

            // 文字列
            if (fieldValue.AllowedLength > 0)
            {
                return (T)Convert.ChangeType
                    (sharedData.Charmap.BytesToString(data, startIndex, entryLength), 
                    typeof(T));
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
                            int nibbleValue = ctrlNameindex == 0 // high
                                ? (rawByte >> Constants.NibbleShift) & Constants.NibbleMask
                                : rawByte & Constants.NibbleMask;
                            return (T)Convert.ChangeType(nibbleValue, typeof(T));

                        // ビット
                        case Constants.BitsPerByte:
                            int bitValue = (rawByte >> ctrlNameindex) & 1;
                            return (T)Convert.ChangeType(bitValue, typeof(T));

                        default:
                            return isSigned
                                ? (T)Convert.ChangeType((sbyte)rawByte, typeof(T))
                                : (T)Convert.ChangeType(rawByte, typeof(T));
                    }

                // 2バイト
                case Constants.UShortSize:
                    ushort rawUShort = (ushort)IoHelper.ReadByteValue(
                        data,
                        startIndex,
                        Constants.UShortSize);
                    return isSigned
                        ? (T)Convert.ChangeType((short)rawUShort, typeof(T))
                        : (T)Convert.ChangeType(rawUShort, typeof(T));

                // 4バイト
                case Constants.UIntSize:
                    uint rawUInt = IoHelper.ReadByteValue(
                        data,
                        startIndex,
                        Constants.UIntSize);

                    // ポインタ
                    if (fieldValue.IsPointer)
                    {
                        return rawUInt == 0
                            ? (T)Convert.ChangeType(Constants.InvalidValue, typeof(T))
                            : (T)Convert.ChangeType(rawUInt - Constants.BaseAddr, typeof(T));
                    }

                    return isSigned
                        ? (T)Convert.ChangeType((int)rawUInt, typeof(T))
                        : (T)Convert.ChangeType(rawUInt, typeof(T));

                // その他
                default:
                    return default;
            }
        }
    }
}
