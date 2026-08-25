using System;

namespace PochiPochiEditor2.Managers.Fields
{
    public class RefData : IRefData
    {
        public Enum Name { get; }
        public int Offset { get; set; }
        public byte[] BinaryData { get; set; }

        public RefData(
            Enum enumKey,
            int offset,
            byte[] binaryData)
        {
            Name = enumKey;
            Offset = offset;
            BinaryData = binaryData;
        }

        public void Set(
            int offset,
            byte[] binaryData)
        {
            Offset = offset;
            BinaryData = (byte[])binaryData.Clone();
        }
    }

    public interface IRefData
    {
        Enum Name { get; }
        int Offset { get; set; }
        byte[] BinaryData { get; set; }
    }
}
