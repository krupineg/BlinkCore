using System;

namespace BlinkCore
{
    public class WiringInitializationException : Exception
    {
        public int ErrorCode { get; }

        public WiringInitializationException(string message, int code) : base(message)
        {
            ErrorCode = code;
        }
    }
}