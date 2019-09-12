using UnityEngine;
using System;
namespace RCG.Maps
{
    public interface ILocatable
    {
        Vector2Int Location { get; }
        event Action<Vector2Int> OnUpdated;
    }
}
