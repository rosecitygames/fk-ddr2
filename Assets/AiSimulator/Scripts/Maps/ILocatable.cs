using UnityEngine;

namespace IndieDevTools.Maps
{
    public interface ILocatable : IUpdatable<ILocatable>
    {
        Vector2Int Location { get; }
    }
}
