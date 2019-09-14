using System;

namespace IndieDevTools
{
    public interface IUpdatable<T>
    {
        event Action<T> OnUpdated;
    }
}