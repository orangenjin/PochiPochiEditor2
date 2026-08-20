using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Utilities
{
    public class UiPipelineBuilder<TData> where TData : new()
    {
        private List<Action<UiContext<TData>>> _actions = new List<Action<UiContext<TData>>>();

        public UiPipelineBuilder<TData> Then(Action<UiContext<TData>> action)
        {
            _actions.Add(action);
            return this;
        }

        public void Execute(UiContext<TData> context)
        {
            foreach (var action in _actions)
            {
                action(context);
            }
        }
    }

    public class UiContext<TData> where TData : new()
    {
        public object Sender { get; }
        public EventArgs EventArgs { get; }
        public TData Data { get; }

        public UiContext(object sender, EventArgs e)
        {
            Sender = sender;
            EventArgs = e;
            Data = new TData();
        }
    }
}
