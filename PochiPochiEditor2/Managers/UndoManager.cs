using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Managers
{
    public class UndoManager
    {
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        // Undo, Redoの発生判定
        public event EventHandler StateChanged = null;

        // 実行可能かどうか、チェック用
        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void PushCommand(ICommand command)
        {
            ExecuteAndNotify(() =>
            {
                _undoStack.Push(command);
                _redoStack.Clear();
            });
        }

        public void Undo()
        {
            if (!CanUndo) return;

            ExecuteAndNotify(() =>
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
            });
        }

        public void Redo()
        {
            if (!CanRedo) return;

            ExecuteAndNotify(() =>
            {
                var command = _redoStack.Pop();
                command.Redo();
                _undoStack.Push(command);
            });
        }

        /// <summary>
        /// 共通処理で、発生したことを知らせる。
        /// </summary>
        private void ExecuteAndNotify(Action action)
        {
            action();
            StateChanged?.Invoke(this, EventArgs.Empty);
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
