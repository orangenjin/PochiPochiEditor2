using System;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Helpers
{
    public static class IoHelper
    {
        /// <summary>
        /// 1, 2, 4バイトくらいしか想定していない。uintで返るので注意。
        /// </summary>
        public static uint ReadByteValue(
            byte[] data, 
            int offset, 
            int length, 
            bool isLittleEndian = true)
        {
            uint result = 0;

            for (int i = 0; i < length; i++)
            {
                int shiftIndex = isLittleEndian 
                    ? i 
                    : (length - 1 - i);

                // 加算
                result |= (uint)data[offset + i] << (shiftIndex * Constants.BitsPerByte);
            }

            return result;
        }

        /// <summary>
        /// uintをbyte[]に書き込む。
        /// </summary>
        public static void WriteByteValue(
            byte[] buffer,
            int offset,
            int length,
            uint value,
            bool isLittleEndian = true)
        {
            for (int i = 0; i < length; i++)
            {
                int shiftIndex = isLittleEndian
                    ? i
                    : (length - 1 - i);

                buffer[offset + i] =
                    (byte)((value >> (shiftIndex * Constants.BitsPerByte)) & Constants.ByteMask);
            }
        }

        /// <summary>
        /// バイト配列からポインタを読み取る。
        /// [00 00 00 00]はnullポインタとして、trueとConstants.InvalidValueを返す。
        /// ポインタとして読み取れない場合は、falseとConstants.InvalidValueを返す。
        /// </summary>
        public static bool TryReadPtr(
            byte[] data,
            int ptrOffset,
            out int resultOffset)
        {
            uint rawAddr = ReadByteValue(data, ptrOffset, Constants.UIntSize, true); // uint

            // nullポインタ?
            if (rawAddr == 0)
            {
                resultOffset = Constants.InvalidValue;
                return true;
            }

            // 正規?
            if (rawAddr < Constants.BaseAddr)
            {
                resultOffset = Constants.InvalidValue;
                return false;
            }

            resultOffset = (int)(rawAddr - Constants.BaseAddr);
            return true;
        }

        /// <summary>
        /// 4の倍数サイズでないバイト配列（画像など）を、アライメント調整して書き込む。
        /// </summary>
        public static void WriteBytesToData(
            byte[] data,
            int offset,
            byte[] bytes,
            byte alignPaddingByte = Constants.PaddingByte)
        {
            Array.Copy(bytes, Constants.DefaultIndex, data, offset, bytes.Length);
            int endOffset = offset + bytes.Length;
            int remainder = endOffset % Constants.UIntSize;

            if (remainder != 0)
            {
                int paddingCount = Constants.UIntSize - remainder;
                for (int i = 0; i < paddingCount; i++)
                {
                    int padOffset = endOffset + i;
                    data[padOffset] = alignPaddingByte;
                }
            }
        }
    }
}
