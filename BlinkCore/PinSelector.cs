using WiringPiNETCore;

namespace BlinkCore
{
    internal class PinSelector : IPinSelector
    {
        private readonly IGpioCore _core;
        private readonly int _pin;

        public PinSelector(IGpioCore core, int pin)
        {
            _core = core;
            _pin = pin;
        }
        
        public IPinInput AsInput()
        {
            _core.Input(_pin);
            return new InputPin(_core, _pin);
        }

        public IPinOutput AsOutput()
        {
            _core.Input(_pin);
            return new OutputPin(_core, _pin);
        }

        private class InputPin : IPinInput
        {
            private readonly IGpioCore _core;
            private readonly int _pin;

            public InputPin(IGpioCore core, int pin)
            {
                _core = core;
                _pin = pin;
            }

            public int Read()
            {
                return _core.Read(_pin);
            }
        }

        private class OutputPin :IPinOutput
        {
            private readonly IGpioCore _core;
            private readonly int _pin;

            public OutputPin(IGpioCore core, int pin)
            {
                _core = core;
                _pin = pin;
            }

            public IPinOutput High()
            {
                _core.Write(_pin, 1);
                return this;
            }

            public IPinOutput Low()
            {
                _core.Write(_pin, 0);
                return this;
            }
        }
    }
}