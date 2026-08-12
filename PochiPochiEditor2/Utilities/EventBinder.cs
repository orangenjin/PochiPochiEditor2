using System;
using System.Collections.Generic;

public class EventBinder : IDisposable
{
    // イベント解除用
    private readonly List<Action> _detachActions = new List<Action>();

    /// <summary>
    /// 通常のイベントの追加と解除。
    /// </summary>
    public void BindCtrl(
        Action<EventHandler> adder,
        Action<EventHandler> remover, 
        EventHandler handler = null)
    {
        // 解除用の時、nullにする
        if (handler == null)
        {
            handler = (s, e) => Dispose();
        }
        adder(handler);
        _detachActions.Add(() => remover(handler));
    }

    /// <summary>
    /// 自作のイベントの追加と解除。
    /// </summary>
    public void BindCustom(Action attachAction, Action detachAction)
    {
        attachAction();
        _detachActions.Add(detachAction);
    }

    /// <summary>
    /// 破棄のタイミングを指定する用。
    /// </summary>
    public void Dispose()
    {
        foreach (var detach in _detachActions)
        {
            detach();
        }

        _detachActions.Clear();
    }
}
