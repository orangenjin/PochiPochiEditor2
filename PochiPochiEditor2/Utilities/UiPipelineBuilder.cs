using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Utilities
{
    public class UiPipelineBuilder
    {
        private readonly List<Action<UiContext>> _actions = new List<Action<UiContext>>();

        /// <summary>
        /// 連鎖的な記述を可能にするため。
        /// </summary>
        public UiPipelineBuilder Then(Action<UiContext> action)
        {
            _actions.Add(action);
            return this;
        }

        public void Execute(UiContext context)
        {
            foreach (var action in _actions)
            {
                action(context);
            }
        }
    }

    public class UiContext
    {
        public object Sender { get; }
        public EventArgs EventArgs { get; }

        // 一時的なデータ
        private readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();

        public UiContext(object sender, EventArgs e)
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
