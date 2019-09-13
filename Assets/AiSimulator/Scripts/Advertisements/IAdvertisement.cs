using IndieDevTools.Attributes;
using IndieDevTools.Maps;
using UnityEngine;
using System.Collections.Generic;

namespace IndieDevTools.Advertisements
{
    public interface IAdvertisement : IAttributeCollection, ILocatable, IGroupMember
    {
        IMap Map { get; }
        List<Vector2Int> BroadcastLocations { get; }
    }
}