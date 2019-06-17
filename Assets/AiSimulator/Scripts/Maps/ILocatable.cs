using UnityEngine;

namespace RCG.Maps
{
    public interface ILocatable
    {
        Vector2Int Location { get; }
    }
}
