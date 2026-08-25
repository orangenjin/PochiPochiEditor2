using PochiPochiEditor2.Managers.Fields;

namespace PochiPochiEditor2.Managers.Commands
{
    public class RefDataChangeCommand : ICommand
    {
        private RefData _refData;

        private int _oldOffset;
        private byte[] _oldData;

        private int _newOffset;
        private byte[] _newData;

        public string Desc { get; }

        public RefDataChangeCommand(
            RefData refData,
            int oldOffset,
            byte[] oldData,
            int newOffset,
            byte[] newData,
            string desc)
        {
            _refData = refData;

            _oldOffset = oldOffset;
            _oldData = (byte[])oldData.Clone(); // 参照を切るためにCloneする

            _newOffset = newOffset;
            _newData = (byte[])newData.Clone();

            Desc = desc;
        }

        public void Undo()
        {
            _refData.Restore(_oldOffset, _oldData);
        }

        public void Redo()
        {
            _refData.Restore(_newOffset, _newData);
        }
    }
}
