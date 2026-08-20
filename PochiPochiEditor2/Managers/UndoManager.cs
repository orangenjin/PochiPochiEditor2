using System.Collections.Generic;

namespace PochiPochiEditor2.Managers
{
    public class UndoManager
    {
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        // 実行可能かどうか、チェック用
        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void PushCommand(ICommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear(); // Redoはクリア
        }

        public void Undo()
        {
            if (!CanUndo) return;
            var command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }

        public void Redo()
        {
            if (!CanRedo) return;
            var command = _redoStack.Pop();
            command.Redo();
            _undoStack.Push(command);
        }
    }

    /// <summary>
    /// Undo, Redoの操作を規定する。
    /// </summary>
    public interface ICommand
    {
        void Undo();
        void Redo();
    }
}
