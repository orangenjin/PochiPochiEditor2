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
            BinaryData = binaryData;
        }

        public void Restore(int offset, byte[] binaryData)
        {
            // 自身のプロパティを更新
            Set(offset, binaryData);

            // RomData に書き戻す
            IoHelper.WriteBytesToData(
                _sharedData.RomData,
                Offset,
                BinaryData);
        }

        public void Update(
            UndoManager undoManager,
            int newOffset,
            byte[] newBinaryData,
            string desc)
        {
            int oldOffset = Offset;

            // 書き込み先の書き込み前状態を格納
            byte[] oldBinary = new byte[newBinaryData.Length];
            Array.Copy(
                _sharedData.RomData, 
                newOffset, 
                oldBinary,
                Constants.DefaultIndex,
                newBinaryData.Length);

            // データの更新
            Restore(newOffset, newBinaryData);

            if (oldOffset != newOffset ||
                !oldBinary.SequenceEqual(newBinaryData))
            {
                var cmd = new RefDataChangeCommand(
                    this,
                    oldOffset,
                    oldBinary,
                    Offset,
                    BinaryData,
                    desc);

                undoManager.PushCommand(cmd);
            }
        }
    }
}
