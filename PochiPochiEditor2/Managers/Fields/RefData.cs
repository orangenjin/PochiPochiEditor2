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

        // 共有データ
        private SharedData _sharedData;

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

        public void Set(int offset, byte[] binaryData)
        {
            Offset = offset;
            BinaryData = binaryData;
        }

        public void WriteData(int offset, byte[] binaryData)
        {
            IoHelper.WriteBytesToData(
                _sharedData.RomData,
                offset,
                binaryData);
        }

        public void Update(
            UndoManager undoManager,
            int newOffset,
            byte[] newBinaryData,
            string desc)
        {
            int oldOffset = Offset;
            byte[] oldBinaryData = BinaryData;

            // 同一の場合
            if (oldOffset == newOffset &&
                oldBinaryData.SequenceEqual(newBinaryData)) return;

            // 書き込み先の状態を保持する
            byte[] oldTargetData = new byte[newBinaryData.Length];
            Array.Copy(
                _sharedData.RomData,
                newOffset,
                oldTargetData,
                Constants.DefaultIndex,
                newBinaryData.Length);

            WriteData(newOffset, newBinaryData);
            Set(newOffset, newBinaryData);

            var command = new RefDataChangeCommand(
                this,
                oldOffset,
                oldBinaryData,
                newOffset,
                newBinaryData,
                oldTargetData,
                desc);

            undoManager.PushCommand(command);
        }
    }
}
