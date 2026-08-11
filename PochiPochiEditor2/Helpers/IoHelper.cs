using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PochiPochiEditor2.Helpers
{
    public static class IoHelper
    {
        public static ushort ReadUShort(byte[] data, int offset, bool isLittleEndian = true)
        {
            if (isLittleEndian)
            {
                return (ushort)(data[offset] | (data[offset + 1] << Constants.BitsPerByte));
            }
            else
            {
                return (ushort)((data[offset] << Constants.BitsPerByte) | data[offset + 1]);
            }
        }

        public static uint ReadUInt(byte[] data, int offset, bool isLittleEndian = true)
        {
            if (isLittleEndian)
            {
                return (uint)data[offset]
                     | ((uint)data[offset + 1] << (Constants.BitsPerByte * 1))
                     | ((uint)data[offset + 2] << (Constants.BitsPerByte * 2))
                     | ((uint)data[offset + 3] << (Constants.BitsPerByte * 3));
            }
            else
            {
                return ((uint)data[offset] << (Constants.BitsPerByte * 3))
                     | ((uint)data[offset + 1] << (Constants.BitsPerByte * 2))
                     | ((uint)data[offset + 2] << (Constants.BitsPerByte * 1))
                     | (uint)data[offset + 3];
            }
        }

        /// <summary>
        /// ポインタとして読み取る。
        /// [00 00 00 00]はnullポインタとして、trueとConstants.InvalidOffsetを返す。
        /// ポインタとして読み取れない場合は、falseとConstants.InvalidOffsetを返す。
        /// </summary>
        public static bool TryReadPtr(
            byte[] data,
            int ptrOffset,
            out int resultOffset)
        {
            uint rawAddr = ReadUInt(data, ptrOffset, true); // uint

            // nullポインタ?
            if (rawAddr == 0)
            {
                resultOffset = Constants.InvalidOffsetValue;
                return true;
            }

            // 正規?
            if (rawAddr < Constants.BaseAddr)
            {
                resultOffset = Constants.InvalidOffsetValue;
                return false;
            }

            resultOffset = (int)(rawAddr - Constants.BaseAddr);
            return true;
        }

        /// <summary>
        /// 4の倍数サイズでないバイト配列を、アライメント調整して書き込む。
        /// </summary>
        public static void WriteBytesToData(
            byte[] data,
            int offset,
            byte[] bytes,
            byte alignPaddingByte = Constants.PaddingByte)
        {
            Array.Copy(bytes, 0, data, offset, bytes.Length);
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
