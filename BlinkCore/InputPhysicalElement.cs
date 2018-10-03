namespace BlinkCore
{
    public class InputPhysicalElement : IInputPhysicalElement
    {
        private readonly IPinInput _pin;

        public InputPhysicalElement(IPinInput pin)
        {
            _pin = pin;
        }

        public int Read()
        {
            return _pin.Read();
        }
    }
}