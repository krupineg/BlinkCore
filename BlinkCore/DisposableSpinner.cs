using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlinkCore
{
    internal sealed class DisposableSpinner : IDisposable
    {
        private readonly IPhysicalElement _physicalElement;
        private readonly int _delay;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Task _task;

        public DisposableSpinner(IPhysicalElement physicalElement, int delay)
        {
            _physicalElement = physicalElement;
            _delay = delay;
            _cancellationTokenSource = new CancellationTokenSource();
            _task = Task.Factory.StartNew(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    physicalElement.On();
                    await Task.Delay(delay);
                    physicalElement.Off();
                    await Task.Delay(delay);
                }
            }, TaskCreationOptions.LongRunning);
        }

        ~DisposableSpinner()
        {
            Dispose();
            Debug.Assert(false, "Spinner was not disposed.");
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _task.Wait(_delay * 10);
            _physicalElement.Off();
        }
    }
}