using System;
using System.Threading;
using System.Threading.Tasks;
using WiringPiNETCore;

namespace BlinkCore
{
    class Program
    {
        private const int Pin = 17;

        static void Main(string[] args)
        {
            Hola();
            Initialize();
            SetMode();

            using (RunBlinks())
            {
                Console.ReadKey();
            }
        }

        private class DisposableBlink : IDisposable
        {
            private readonly CancellationTokenSource _cancellationTokenSource;
            private readonly Task _task;

            public DisposableBlink()
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _task = Task.Factory.StartNew(() =>
                {
                    while (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        GPIO.digitalWrite(Pin, (int)GPIO.GPIOpinvalue.High);
                        Thread.Sleep(1000);
                        GPIO.digitalWrite(Pin, (int)GPIO.GPIOpinvalue.Low);
                        Thread.Sleep(1000);
                    }
                }, TaskCreationOptions.LongRunning);
            }

            public void Dispose()
            {
                _cancellationTokenSource.Cancel();
                _task.Wait(2000);
            }
        }

        private static IDisposable RunBlinks()
        {
            return new DisposableBlink();
        }


        private static void Hola()
        {
            Console.WriteLine("Let's blink");
        }

        private static void SetMode()
        {
            GPIO.pinMode(Pin, (int)GPIO.GPIOpinmode.Output);
        }

        private static void Initialize()
        {
            var setup = Init.WiringPiSetup();
            Console.WriteLine($"setup: {setup}");
            var setupGpio = Init.WiringPiSetupGpio();
            Console.WriteLine($"setupGpio: {setupGpio}");
        }
    }
}
