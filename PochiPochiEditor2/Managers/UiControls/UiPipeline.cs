using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Managers.UiControls
{
    public class UiPipeline<TData> where TData : new()
    {
        private List<Action<UiContext<TData>>> _actions = new List<Action<UiContext<TData>>>();

        public UiPipeline<TData> Then(Action<UiContext<TData>> action)
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
        public UpdateReason Reason { get; }
        public TData Data { get; }

        public UiContext(object sender, EventArgs e, UpdateReason reason)
        {
            Sender = sender;
            EventArgs = e;
            Reason = reason;
            Data = new TData();
        }
    }

    public enum UpdateReason
    {
        Ctrl,
        Model,
        Init
    }
}
