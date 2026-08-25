using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class RefDataManager<T> : IRefData
    {
        private byte[] _binaryData = null;

        private Func<byte[], T> _decoder = null;
        private Func<T, byte[]> _encoder = null;

        // 共有データ用
        private SharedData _sharedData = null;

        // 公開用
        public Enum Name { get; }
        public byte[] BinaryData　=> _binaryData;
        public int Length => _binaryData.Length;
        public int Offset { get; set; }

        public RefDataManager(
            Enum enumKey,
            SharedData sharedData,
            Func<byte[], T> decoder,
            Func<T, byte[]> encoder)
        {
            Name = enumKey;

            _sharedData = sharedData;
            _decoder = decoder;
            _encoder = encoder;
        }
    }

    public interface IRefData
    {
        Enum Name { get; }
        byte[] BinaryData { get; }
        int Length { get; }
        int Offset { get; set; }
    }
}
