namespace BlinkCore
{
    public class OutputPhysicalElement : IPhysicalElement
    {
        private readonly IPinOutput _pin;

        public OutputPhysicalElement(IPinOutput pin)
        {
            _pin = pin;
        }

        public void On()
        {
            _pin.High();
        }


        public void Off()
        {
            _pin.Low();
        }
    }
}