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
            int valueindex,
            TblManager charmap)
        {
            int entryLength = fieldValue.EntryLength;
            byte[] binaryData = fieldValue.BinaryData;
            bool isSigned = fieldValue.IsSigned;

            // 文字列
            if (fieldValue.AllowedLength > 0)
            {
                return (T)Convert.ChangeType
                    (charmap.BytesToString(binaryData, Constants.DefaultIndex, entryLength), 
                    typeof(T));
            }

            switch(entryLength)
            {
                // 1バイト
                case Constants.ByteSize:
                    byte rawByte = (byte)IoHelper.ReadByteValue(
                        binaryData,
                        Constants.DefaultIndex,
                        Constants.ByteSize);
                    switch (fieldValue.ValueCount)
                    {
                        // ニブル
                        case Constants.CharPerByte:
                            int nibbleValue =
                                valueindex == (int)FieldExtensions.NibbleAttrArgs.HighValueArg // high
                                ? (rawByte >> Constants.NibbleShift) & Constants.NibbleMask
                                : rawByte & Constants.NibbleMask;
                            return (T)Convert.ChangeType(nibbleValue, typeof(T));

                        // ビット
                        case Constants.BitsPerByte:
                            int bitValue = (rawByte >> valueindex) & 1;
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
                        Constants.DefaultIndex,
                        Constants.UShortSize);
                    return isSigned
                        ? (T)Convert.ChangeType((short)rawUShort, typeof(T))
                        : (T)Convert.ChangeType(rawUShort, typeof(T));

                // 4バイト
                case Constants.UIntSize:
                    uint rawUInt = IoHelper.ReadByteValue(
                        binaryData,
                        Constants.DefaultIndex,
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
           int ctrlNameindex,
           TblManager charmap)
        {
            int entryLength = fieldValue.EntryLength;
            bool isSigned = fieldValue.IsSigned;

            // マージ用
            byte[] result = new byte[entryLength];
            Array.Copy(fieldValue.BinaryData,
                Constants.DefaultIndex, 
                result, 
                Constants.DefaultIndex, 
                entryLength);

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
                Array.Copy(bytes, 
                    Constants.DefaultIndex, 
                    result, 
                    Constants.DefaultIndex, 
                    Math.Min(bytes.Length, entryLength));

                return result;
            }

            switch (entryLength)
            {
                // 1バイト
                case Constants.ByteSize:
                    byte rawByte = result[Constants.DefaultIndex];

                    switch (fieldValue.ValueCount)
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
                            result[Constants.DefaultIndex] = rawByte;
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
                            result[Constants.DefaultIndex] = rawByte;
                            break;

                        default:
                            result[Constants.DefaultIndex] = isSigned
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
                    IoHelper.WriteByteValue(result, Constants.DefaultIndex, Constants.UShortSize, rawUShort);
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
                    IoHelper.WriteByteValue(result, Constants.DefaultIndex, Constants.UIntSize, rawUInt);
                    break;
            }

            return result;
        }

        public static string TextLengthValidate(TblManager charmap, string text, int entryLength)
        {
            // 空白ならそのまま返す
            if (string.IsNullOrEmpty(text)) return text;

            // 規定長を取得
            int maxBytes = entryLength - 1;

            // 現在の長さを取得
            byte[] currentBytes = charmap.StringToBytes(text, false);

            // 範囲内ならそのまま返す
            if (currentBytes.Length <= maxBytes) return text;

            // StringInfoで分割
            StringInfo stringInfo = new StringInfo(text);
            int count = stringInfo.LengthInTextElements;

            // 一文字ずつ削る
            string currentText = text;
            while (count > 0)
            {
                // 末尾一文字を除いた文字列
                count--;
                currentText = stringInfo.SubstringByTextElements(0, count);

                // バイト数をチェック
                byte[] bytes = charmap.StringToBytes(currentText, false);
                if (bytes.Length <= maxBytes) break;
            }

            return currentText;
        }
    }
}
