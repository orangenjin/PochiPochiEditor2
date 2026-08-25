using System.Collections.Generic;

namespace PochiPochiEditor2.Managers.Commands
{
    public class CombineCommand
    {
        private List<ICommand> _commands = null;

        public string Desc { get; }

        public CombineCommand(List<ICommand> commands, string desc)
        {
            _commands = new List<ICommand>(commands);
            Desc = desc;
        }

        public void Undo()
        {
            // 一応逆順処理
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }

        public void Redo()
        {
            foreach (var command in _commands)
            {
                command.Redo();
            }
        }
    }
}
