using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlinkCore
{
    internal sealed class SequenceObserver : Observable<Timestamped<int>>
    {
        private readonly Task _task;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public SequenceObserver(IInputPhysicalElement physicalElement, int delay)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _task = Task.Factory.StartNew(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Push(new Timestamped<int>(physicalElement.Read(), DateTime.Now.Ticks));
                    await Task.Delay(delay);
                }
            }, TaskCreationOptions.LongRunning);
        }

        protected override void BeforeDispose()
        {
            _cancellationTokenSource.Cancel();
            _task.Wait();
            _cancellationTokenSource.Dispose();
            _task.Dispose();
        }
    }
}