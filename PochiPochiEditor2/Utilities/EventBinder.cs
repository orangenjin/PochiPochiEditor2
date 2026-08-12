using System;
using System.Collections.Generic;

public class EventBinder : IDisposable
{
    // イベント解除用
    private readonly List<Action> _detachActions = new List<Action>();

    /// <summary>
    /// 通常のイベントの追加と解除。
    /// </summary>
    public void BindCtrl<T>(Action<T> adder, Action<T> remover, T handler) where T : Delegate
    {
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
    /// 破棄タイミングを指定する。
    /// </summary>
    public void Dispose()
    {
        foreach (var detach in _detachActions)
        {
            detach();
        }

        //　念のためクリア
        _detachActions.Clear();
    }
}
