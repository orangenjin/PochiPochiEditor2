namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldChangeCommand : ICommand
    {
        private FieldValue _target = null;
        private byte[] _oldData = null;
        private byte[] _newData = null;

        public string Desc { get; }

        public FieldChangeCommand
            (FieldValue target, 
            byte[] oldData, 
            byte[] newData,
            string description)
        {
            _target = target;
            _oldData = (byte[])oldData.Clone(); // 参照を切るためにCloneする
            _newData = (byte[])newData.Clone();
            Desc = description;
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
