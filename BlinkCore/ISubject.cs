using System;

namespace BlinkCore
{
    public interface ISubject<in T> : IDisposable
    {
        void Push(T value);
    }
}