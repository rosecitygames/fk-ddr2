using UnityEngine;

namespace RCG.Maps
{
    public interface ILocatable
    {
        Vector3Int Location { get; }
    }
}
