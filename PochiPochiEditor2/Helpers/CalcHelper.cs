using System;
using System.Globalization;

using PochiPochiEditor2.Managers;
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
        /// byte[]を型Tとして変換する。
        /// </summary>
        public static T BytesToModelConv<T>(
            FieldValue fieldValue,
            TblManager charmap,
            int ctrlNameindex)
        {
            int entryLength = fieldValue.EntryLength;
            byte[] binaryData = fieldValue.BinaryData;
            int nameCount = fieldValue.ControlNames.Length;
            bool isSigned = fieldValue.IsSigned;

            // 文字列
            if (fieldValue.AllowedLength > 0)
            {
                return (T)Convert.ChangeType
                    (charmap.BytesToString(binaryData, 0, entryLength), 
                    typeof(T));
            }

            switch(entryLength)
            {
                // 1バイト
                case Constants.ByteSize:
                    byte rawByte = (byte)IoHelper.ReadByteValue(
                        binaryData,
                        0,
                        Constants.ByteSize);
                    switch (nameCount)
                    {
                        // ニブル
                        case Constants.CharPerByte:
                            int nibbleValue = 
                                ctrlNameindex == (int)FieldExtensions.NibbleAttrArgs.HighValueArg // high
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
                        binaryData,
                        0,
                        Constants.UShortSize);
                    return isSigned
                        ? (T)Convert.ChangeType((short)rawUShort, typeof(T))
                        : (T)Convert.ChangeType(rawUShort, typeof(T));

                // 4バイト
                case Constants.UIntSize:
                    uint rawUInt = IoHelper.ReadByteValue(
                        binaryData,
                        0,
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

        /// <summary>
        /// 型Tの値をbyte[]に変換する。
        /// </summary>
        public static byte[] ModelToBytesConv<T>(
           T value,
           FieldValue fieldValue,
           TblManager charmap,
           int ctrlNameindex)
        {
            int entryLength = fieldValue.EntryLength;
            int nameCount = fieldValue.ControlNames.Length;
            bool isSigned = fieldValue.IsSigned;

            // マージ用
            byte[] result = new byte[entryLength];
            Array.Copy(fieldValue.BinaryData, 0, result, 0, entryLength);

            // 文字列
            if (fieldValue.AllowedLength > 0)
            {
                var text = Convert.ToString(value);

                // AllowedLength分
                byte[] bytes = charmap.StringToBytes(
                    text,
                    appendTerminator: true,
                    targetLength: fieldValue.AllowedLength);

                // EntryLength分
                Array.Copy(bytes, 0, result, 0, Math.Min(bytes.Length, entryLength));
                return result;
            }

            switch (entryLength)
            {
                // 1バイト
                case Constants.ByteSize:
                    byte rawByte = result[0];

                    switch (nameCount)
                    {
                        // ニブル（上位/下位のニブルのみ更新）
                        case Constants.CharPerByte:
                            byte nibbleValue = Convert.ToByte(value);
                            if (ctrlNameindex == (int)FieldExtensions.NibbleAttrArgs.HighValueArg) // high
                            {
                                // 下位ニブルを残し、上位ニブルに値をセット
                                rawByte = (byte)((rawByte & ~(Constants.NibbleMask << Constants.NibbleShift))
                                               | ((nibbleValue & Constants.NibbleMask) << Constants.NibbleShift));
                            }
                            else // low
                            {
                                // 上位ニブルを残し、下位ニブルに値をセット
                                rawByte = (byte)((rawByte & (Constants.NibbleMask << Constants.NibbleShift))
                                               | (nibbleValue & Constants.NibbleMask));
                            }
                            result[0] = rawByte;
                            break;

                        // ビット（特定の1ビットのみ更新）
                        case Constants.BitsPerByte:
                            byte bitValue = Convert.ToByte(value);
                            if ((bitValue & 1) == 1)
                            {
                                rawByte |= (byte)(1 << ctrlNameindex);
                            }
                            else
                            {
                                rawByte &= (byte)~(1 << ctrlNameindex);
                            }
                            result[0] = rawByte;
                            break;

                        default:
                            result[0] = isSigned
                                ? (byte)Convert.ToSByte(value)
                                : Convert.ToByte(value);
                            break;
                    }
                    break;

                // 2バイト
                case Constants.UShortSize:
                    ushort rawUShort = isSigned
                        ? (ushort)Convert.ToInt16(value)
                        : Convert.ToUInt16(value);
                    IoHelper.WriteByteValue(result, 0, Constants.UShortSize, rawUShort);
                    break;

                // 4バイト
                case Constants.UIntSize:
                    uint rawUInt;

                    if (fieldValue.IsPointer)
                    {
                        int tempValue = Convert.ToInt32(value);
                        rawUInt = tempValue == Constants.InvalidValue
                            ? 0
                            : (uint)tempValue + Constants.BaseAddr;
                    }
                    else
                    {
                        rawUInt = isSigned
                            ? (uint)Convert.ToInt32(value)
                            : Convert.ToUInt32(value);
                    }
                    IoHelper.WriteByteValue(result, 0, Constants.UIntSize, rawUInt);
                    break;
            }

            return result;
        }
    }
}
