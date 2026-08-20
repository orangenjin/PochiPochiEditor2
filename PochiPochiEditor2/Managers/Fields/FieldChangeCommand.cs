using System;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldChangeCommand : ICommand
    {
        private readonly FieldValue _target = null;
        private readonly byte[] _oldData = null;
        private readonly byte[] _newData = null;

        public FieldChangeCommand
            (FieldValue target, 
            byte[] oldData, 
            byte[] newData)
        {
            _target = target;
            _oldData = (byte[])oldData.Clone(); // 参照を切るためにCloneする
            _newData = (byte[])newData.Clone();
        }

        public void Undo()
        {
            _target.BinaryData = _oldData;
        }

        public void Redo()
        {
            _target.BinaryData = _newData;
        }
    }
}
