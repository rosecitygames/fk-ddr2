using System;

namespace IndieDevTools.Common
{
    public interface IUpdatable
    {
        event Action OnUpdated;
    }
}