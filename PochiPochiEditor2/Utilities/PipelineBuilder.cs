using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Utilities
{
    public class PipelineBuilder
    {
        private readonly List<Action<Context>> _actions = new List<Action<Context>>();

        /// <summary>
        /// 連鎖的な記述を可能にするため。
        /// </summary>
        public PipelineBuilder Then(Action<Context> action)
        {
            _actions.Add(action);
            return this;
        }

        public void Execute(Context context)
        {
            foreach (var action in _actions)
            {
                action(context);
            }
        }
    }

    public class Context
    {
        public object Sender { get; }
        public EventArgs EventArgs { get; }

        // 一時的なデータ、キーは型名
        private readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();

        public Context(object sender, EventArgs e)
        {
            Sender = sender;
            EventArgs = e;
        }

        // 型は一種類ずつしか保持できない
        public void Set<T>(T value)
        {
            _data[typeof(T)] = value;
        }

        public T Get<T>()
        {
            return (T)_data[typeof(T)];
        }
    }
}
