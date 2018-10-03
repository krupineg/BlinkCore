using System;

namespace BlinkCore
{
    public static class SensorExtensions
    {
        public static IPhysicalElement AsPhysicalElement(this IPinOutput pin)
        {
            return new OutputPhysicalElement(pin);
        }

        public static IInputPhysicalElement AsPhysicalElement(this IPinInput pin)
        {
            return new InputPhysicalElement(pin);
        }

        public static IDisposable SpinEach(this IPhysicalElement element, int milliseconds)
        {
            return new DisposableSpinner(element, milliseconds);
        }

        public static IDisposable Observe(this IInputPhysicalElement element, int milliseconds, Action<Timestamped<int>> callback)
        {
            return new SequenceObserver(element, milliseconds).Subscribe(new DelegateObserver(callback));
        }

        private class DelegateObserver :IObserver<Timestamped<int>>
        {
            private readonly Action<Timestamped<int>> _callback;

            public DelegateObserver(Action<Timestamped<int>> callback)
            {
                _callback = callback;
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
             
            }

            public void OnNext(Timestamped<int> value)
            {
                _callback(value);
            }
        }
    }
}