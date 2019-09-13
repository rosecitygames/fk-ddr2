using UnityEngine;
using System;
namespace IndieDevTools.Maps
{
    public interface ILocatable
    {
        Vector2Int Location { get; }
        event Action<Vector2Int> OnUpdated;
    }
}
