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
        /// byte[](FieldValue.BinaryData)を型Tとして変換する。
        /// </summary>
        public static T BytesToModelConv<T>(
            FieldValue fieldValue,
            int argIndex,
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
                        entryLength);

                    // サイズ取得
                    int nibbleSize = FieldExtensions.AttrKind.NibbleAttr.GetAttrCount();
                    int bitSize = FieldExtensions.AttrKind.BitAttr.GetAttrCount();

                    // ニブル
                    if (fieldValue.ArgCount == nibbleSize)
                    {
                        int nibbleValue = argIndex == (int)FieldExtensions.NibbleAttrArgs.HighValueArg
                            ? (rawByte >> Constants.NibbleShift) & Constants.NibbleMask
                            : rawByte & Constants.NibbleMask;

                        return (T)Convert.ChangeType(nibbleValue, typeof(T));
                    }
                    // ビット
                    else if (fieldValue.ArgCount == bitSize)
                    {
                        int bitValue = (rawByte >> argIndex) & 1;
                        return (T)Convert.ChangeType(bitValue, typeof(T));
                    }

                    // デフォルト
                    return isSigned
                        ? (T)Convert.ChangeType((sbyte)rawByte, typeof(T))
                        : (T)Convert.ChangeType(rawByte, typeof(T));

                // 2バイト
                case Constants.UShortSize:
                    ushort rawUShort = (ushort)IoHelper.ReadByteValue(
                        binaryData,
                        Constants.DefaultIndex,
                        entryLength);
                    return isSigned
                        ? (T)Convert.ChangeType((short)rawUShort, typeof(T))
                        : (T)Convert.ChangeType(rawUShort, typeof(T));

                // 4バイト
                case Constants.UIntSize:
                    uint rawUInt = IoHelper.ReadByteValue(
                        binaryData,
                        Constants.DefaultIndex,
                        entryLength);

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
        /// 型Tの値をbyte[](FieldValue.BinaryData)に変換する。
        /// </summary>
        public static byte[] ModelToBytesConv<T>(
           T value,
           FieldValue fieldValue,
           int argIndex,
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
                    byte rawByte = result[Constants.DefaultIndex]; // マージするため

                    switch (fieldValue.ArgCount)
                    {
                        // ニブル（上位/下位のニブルのみ更新）
                        case Constants.CharPerByte:
                            byte nibbleValue = Convert.ToByte(value);

                            // high
                            if (argIndex == (int)FieldExtensions.NibbleAttrArgs.HighValueArg)
                            {
                                // 下位ニブルを残し、上位ニブルに値をセット
                                rawByte = (byte)((rawByte & ~(Constants.NibbleMask << Constants.NibbleShift))
                                               | ((nibbleValue & Constants.NibbleMask) << Constants.NibbleShift));
                            }
                            // low
                            else
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
                                rawByte |= (byte)(1 << argIndex);
                            }
                            else
                            {
                                rawByte &= (byte)~(1 << argIndex);
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

        /// <summary>
        /// 文字列の長さを最大長まで削る。
        /// </summary>
        public static string TextLengthValidate(TblManager charmap, string text, int length)
        {
            // 空白ならそのまま返す
            if (string.IsNullOrEmpty(text)) return text;

            // 規定長を取得
            int maxBytes = length - 1;

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
                // 末尾一文字を削った文字列
                count--;
                currentText = stringInfo.SubstringByTextElements(Constants.DefaultIndex, count);

                // バイト数をチェック
                byte[] bytes = charmap.StringToBytes(currentText, false);
                if (bytes.Length <= maxBytes) break;
            }

            return currentText;
        }

        /// <summary>
        /// 16進数stringからintへ変換する。
        /// </summary>
        public static int ParseStringToInt(this string str)
        {
            // null, 空白である場合
            if (string.IsNullOrEmpty(str))
            {
                return Constants.InvalidValue;
            }

            // 字詰め
            var trimStr = str.Replace(Constants.SpaceChar.ToString(), string.Empty);

            // 変換テスト
            return int.TryParse(trimStr, NumberStyles.HexNumber, null, out int value)
                    ? value
                    : Constants.InvalidValue; // 変換失敗時
        }

        /// <summary>
        /// intから16進数stringから変換する。
        /// </summary>
        public static string ParseIntToString(
            this int val, 
            int digits = Constants.OffsetDigits)
        {
            return val == Constants.InvalidValue
                ? string.Empty
                : val.ToString($"X{digits}");
        }
    }
}
