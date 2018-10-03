using System;
using System.Collections.Generic;
using System.Linq;

namespace BlinkCore
{
    class Program
    {
        private const int Pin = 17;

        static void Main(string[] args)
        {
            Hola();
            var core = args.Contains("debug") ? (IGpioCore)new PseudoGpio(): new GpioCore();
            var spin = new PinProvider(core)
                .Get(Pin)
                .AsOutput()
                .AsPhysicalElement()
                .SpinEach(1000);

            var sequence = new PinProvider(core)
                .Get(Pin)
                .AsInput()
                .AsPhysicalElement()
                .Observe(50,
                    timestamped =>
                        Console.WriteLine(string.Format("received {0} ts: {1}", timestamped.Value,
                            timestamped.Timestamp)));
            using (spin)
            {
                Console.ReadKey();
            }
        }
        
        private static void Hola()
        {
            Console.WriteLine("Let's blink");
        }
    }

    internal class PseudoGpio : IGpioCore
    {
        readonly Dictionary<int, int> _dictionary = new Dictionary<int, int>();
        
        public void Input(int pin)
        {

        }

        public void Output(int pin)
        {
        }

        public void Write(int pin, int value)
        {
            _dictionary[pin] = value;

            Console.WriteLine(string.Format("sent: {0} on pin {1}", value, pin));
        }

        public int Read(int pin)
        {
            return _dictionary[pin];
        }

        private class PseudoElement : IInputPhysicalElement, IPhysicalElement
        {
            private int _value = -1;

            public int Read()
            {
                return _value;
            }

            public void On()
            {
                _value = 1;
            }

            public void Off()
            {
                _value = 0;
                Console.WriteLine("off");
            }
        }
    }
}
