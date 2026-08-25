using System;
using System.Linq;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Commands;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class RefData
    {
        public Enum Name { get; }
        public int Offset { get; set; }
        public byte[] BinaryData { get; set; }

        // 共有データ用
        private SharedData _sharedData = null;

        public RefData(
            Enum enumKey,
            int offset,
            byte[] binaryData,
            SharedData sharedData)
        {
            Name = enumKey;
            _sharedData = sharedData;

            Set(offset, binaryData);
        }

        public void Set(
            int offset,
            byte[] binaryData)
        {
            Offset = offset;
            BinaryData = (byte[])binaryData.Clone();

            IoHelper.WriteBytesToData(
                _sharedData.RomData,
                Offset,
                BinaryData);
        }

        public void Update(
            UndoManager undoManager,
            byte[] binaryData,
            string desc)
        {
            int oldOffset = Offset;
            byte[] oldBinary = BinaryData;

            Set(Offset, binaryData);

            int newOffset = Offset;
            byte[] newBinary = BinaryData;

            if (oldOffset != newOffset ||
                !oldBinary.SequenceEqual(newBinary))
            {
                var cmd = new RefDataChangeCommand(
                    this,
                    oldOffset,
                    oldBinary,
                    newOffset,
                    newBinary,
                    desc);

                undoManager.PushCommand(cmd);
            }
        }
    }
}
