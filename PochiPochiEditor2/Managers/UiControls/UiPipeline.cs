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
        public UpdateTrigger Trigger { get; }
        public TData Data { get; }

        public UiContext(object sender, EventArgs e, UpdateTrigger trigger)
        {
            Sender = sender;
            EventArgs = e;
            Trigger = trigger;
            Data = new TData();
        }
    }

    public enum UpdateTrigger
    {
        Ctrl,
        Model
    }
}
