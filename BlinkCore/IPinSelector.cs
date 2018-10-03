namespace BlinkCore
{
    public interface IPinSelector
    {
        IPinInput AsInput();
        IPinOutput AsOutput();
    }
}