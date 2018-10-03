namespace BlinkCore
{
    internal class PinProvider
    {
        private readonly IGpioCore _core;

        public PinProvider(IGpioCore core)
        {
            _core = core;
        }

        public IPinSelector Get(int pin)
        {
            return new PinSelector(_core, pin);
        }
    }
}