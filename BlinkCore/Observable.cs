using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BlinkCore
{
    public abstract class Observable<T> : IObservable<T>, ISubject<T>
    {
        public IList<DisposableAction<T>> _subscribers = new List<DisposableAction<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var disposableAction = new DisposableAction<T>(observer, _subscribers);
            _subscribers.Add(disposableAction);
            return disposableAction;
        }

        public void Push(T value)
        {
            foreach (var disposableAction in _subscribers)
            {
                disposableAction.Callback(value);
            }
        }

        protected abstract void BeforeDispose();

        public void Dispose()
        {
            BeforeDispose();
            var snapshot = _subscribers.ToImmutableList();
            snapshot.ForEach(x => x.Dispose());
        }
    }
}