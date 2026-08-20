using System;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldChangeCommand : ICommand
    {
        private readonly FieldValue _target;
        private readonly byte[] _oldData;
        private readonly byte[] _newData;
        private readonly Action _onStateRestored; // UI更新用のコールバック

        public FieldChangeCommand
            (FieldValue target, 
            byte[] oldData, 
            byte[] newData, 
            Action onStateRestored)
        {
            _target = target;
            _oldData = (byte[])oldData.Clone(); // 参照を切るためにCloneする
            _newData = (byte[])newData.Clone();
            _onStateRestored = onStateRestored;
        }

        public void Undo()
        {
            _target.BinaryData = _oldData;
            _onStateRestored?.Invoke();
        }

        public void Redo()
        {
            _target.BinaryData = _newData;
            _onStateRestored?.Invoke();
        }
    }
}
