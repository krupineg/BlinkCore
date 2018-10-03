namespace BlinkCore
{
    internal interface IGpioCore
    {
        void Input(int pin);
        void Output(int pin);
        void Write(int pin, int value);
        int Read(int pin);
    }
}