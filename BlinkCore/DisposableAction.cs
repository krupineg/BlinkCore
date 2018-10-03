using System;
using System.Collections.Generic;

namespace BlinkCore
{
    public class DisposableAction<T> : IDisposable
    {
        private readonly IObserver<T> _action;
        private readonly IList<DisposableAction<T>> _list;

        public DisposableAction(IObserver<T> observer, IList<DisposableAction<T>> list)
        {
            _action = observer;
            _list = list;
        }

        public void Callback(T item)
        {
            try
            {
                _action.OnNext(item);
            }
            catch (Exception e)
            {
                _action.OnError(e);
            }
        }

        public void Dispose()
        {
            _action.OnCompleted();
            _list.Remove(this);
        }
    }
}