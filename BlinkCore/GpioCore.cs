using System;
using System.Collections.Generic;
using System.Diagnostics;
using WiringPiNETCore;

namespace BlinkCore
{
    internal sealed class GpioCore : IGpioCore
    {
        private int InputMode = 1;
        private int OutputMode = 0;

        private IDictionary<int, int> _modes = new Dictionary<int, int>();

        public GpioCore()
        {
            var setup = Init.WiringPiSetup();
            if (setup != 0)
            {
                throw new WiringInitializationException("Unable to initialize pi wiring", setup);
            }
            var setupGpio = Init.WiringPiSetupGpio();
            if (setup != 0)
            {
                throw new WiringInitializationException("Unable to initialize pi GPIO", setupGpio);
            }
        }

        public void Input(int pin)
        {
            EnsureModeIsNotSet(pin);
            GPIO.pinMode(pin, (int)GPIO.GPIOpinmode.Input);
            _modes[pin] = InputMode;
        }

        private void EnsureModeIsNotSet(int pin)
        {
            if (_modes.ContainsKey(pin))
            {
                throw new InvalidOperationException("Unable to change pin mode");
            }
        }

        public void Output(int pin)
        {
            EnsureModeIsNotSet(pin);
            GPIO.pinMode(pin, (int)GPIO.GPIOpinmode.Output);
            _modes[pin] = OutputMode;
        }

        public void Write(int pin, int value)
        {
            Debug.Assert(_modes[pin] == OutputMode);
            GPIO.digitalWrite(pin, value);
        }

        public int Read(int pin)
        {
            Debug.Assert(_modes[pin] == InputMode);
            return GPIO.digitalRead(pin);
        }
    }
}