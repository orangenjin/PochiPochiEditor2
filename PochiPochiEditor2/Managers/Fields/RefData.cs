using System;
using System.Linq;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Commands;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    /// <summary>
    /// フィールドでない可変長データを扱う。
    /// </summary>
    public class RefData
    {
        public Enum Name { get; }
        public int Offset { get; set; }
        public byte[] BinaryData { get; set; }

        // 共有データ
        private SharedData _sharedData = null;

        public RefData(
            Enum enumKey,
            int offset,
            byte[] binaryData,
            SharedData sharedData)
        {
            Name = enumKey;
            _sharedData = sharedData;

            SetData(offset, binaryData);
        }

        public void SetData(int offset, byte[] binaryData)
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

        public void UpdateData(
            UndoManager undoManager,
            int newOffset,
            byte[] newBinaryData,
            string desc)
        {
            var command = CreateUpdateCommand(
                newOffset,
                newBinaryData,
                desc);

            if (command != null)
            {
                undoManager.PushCommand(command);
            }
        }

        /// <summary>
        /// コマンドを生成する。
        /// </summary>
        public ICommand CreateUpdateCommand(
            int newOffset,
            byte[] newBinaryData,
            string desc)
        {
            int oldOffset = Offset;
            byte[] oldBinaryData = (byte[])BinaryData.Clone();

            // 同一なら無視
            if (oldOffset == newOffset &&
                oldBinaryData.SequenceEqual(newBinaryData)) return null;

            // 新しい書き込み先の変更前データ
            byte[] oldTargetData = new byte[newBinaryData.Length];
            Array.Copy(
                _sharedData.RomData,
                newOffset,
                oldTargetData,
                Constants.DefaultIndex,
                newBinaryData.Length);

            // データを更新
            WriteData(newOffset, newBinaryData);
            SetData(newOffset, newBinaryData);

            return new RefDataChangeCommand(
                this,
                oldOffset,
                oldBinaryData,
                newOffset,
                newBinaryData,
                oldTargetData,
                desc);
        }
    }
}
