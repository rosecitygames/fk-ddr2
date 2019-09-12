using RCG.Attributes;
using UnityEngine;

namespace RCG.Maps
{
    public interface IMapElement : ILocatable, IPositionable, IDescribable, IStatsCollection, IGroupMember
    {
        int InstanceId { get; }
        IMap Map { get; set; }
        void AddToMap();
        void AddToMap(IMap map);
        void RemoveFromMap();
        bool IsOnMap { get; }
        float Distance(IMapElement otherMapElement);
        float Distance(Vector2Int otherLocation);
        int SortingOrder { get; }
    }
}
