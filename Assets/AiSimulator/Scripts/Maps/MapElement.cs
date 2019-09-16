using IndieDevTools.Traits;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Maps
{
    public class MapElement : IMapElement
    {

        IMap IMapElement.Map { get => Map; set => Map = value; }
        protected IMap Map { get; set; }

        void IMapElement.AddToMap() => Map.AddElement(this);
        void IMapElement.AddToMap(IMap map) => Map.AddElement(this);
        void IMapElement.RemoveFromMap() => Map.RemoveElement(this);

        bool IMapElement.IsOnMap
        {
            get
            {
                if (Map == null) return false;
                return Map.GetIsElementOnMap(this);
            }
        }

        float IMapElement.Distance(IMapElement otherMapElement) => Distance(otherMapElement.Location);
        float IMapElement.Distance(Vector2Int otherLocation) => Distance(otherLocation);
        float Distance(Vector2Int otherLocation)
        {
            return Vector2Int.Distance(otherLocation, Location);
        }

        int IMapElement.InstanceId => InstanceId;
        protected int InstanceId { get; }

        int IMapElement.SortingOrder => SortingOrder;
        protected int SortingOrder { get; }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected int GroupId { get; set; }

        Vector2Int ILocatable.Location => Location;
        protected Vector2Int Location { get; set; }

        event Action<ILocatable> IUpdatable<ILocatable>.OnUpdated { add { OnLocationUpdated += value; } remove { OnLocationUpdated -= value; } }
        protected Action<ILocatable> OnLocationUpdated;

        string IDescribable.DisplayName { get => DisplayName; set => DisplayName = value; }
        string displayName;
        protected string DisplayName
        {
            get => displayName;
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnDescribableUpdated?.Invoke(this);
                }
            }
        }

        string IDescribable.Description { get => Description; set => Description = value; }
        string description;
        protected string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnDescribableUpdated?.Invoke(this);
                }
            }
        }

        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { OnDescribableUpdated += value; } remove { OnDescribableUpdated -= value; } }
        Action<IDescribable> OnDescribableUpdated;

        Vector3 IPositionable.Position { get => Position; set => Position = value; }
        protected virtual Vector3 Position
        {
            get
            {
                return Map.CellToLocal(Location);
            }
            set
            {
                Vector2Int newLocation = Map.LocalToCell(value);
                if (Location != newLocation)
                {
                    Map.AddElement(this);
                    OnLocationUpdated?.Invoke(this);
                }
            }
        }

        List<ITrait> IStatsCollection.Stats { get => Stats.Traits; }
        ITraitCollection Stats { get => stats as ITraitCollection; }
        protected TraitCollection stats = new TraitCollection();

        ITrait IStatsCollection.GetStat(string id)
        {
            return Stats.GetTrait(id);
        }

    }
}

