using IndieDevTools.Attributes;
using UnityEngine;

namespace IndieDevTools.Maps
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
