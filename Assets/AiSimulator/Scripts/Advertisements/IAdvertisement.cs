using RCG.Attributes;
using RCG.Maps;
using UnityEngine;
using System.Collections.Generic;

namespace RCG.Advertisements
{
    public interface IAdvertisement : IAttributeCollection, ILocatable, IGroupMember
    {
        IMap Map { get; }
        List<Vector3Int> BroadcastLocations { get; }
    }
}