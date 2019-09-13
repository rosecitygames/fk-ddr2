﻿using System;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Attributes;

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

        int IGroupMember.GroupId => GroupId;
        protected int GroupId { get; set; }

        Vector2Int ILocatable.Location => Location;
        protected Vector2Int Location { get; set; }

        event Action<Vector2Int> ILocatable.OnUpdated { add { OnLocationUpdated += value; } remove { OnLocationUpdated -= value; } }
        protected Action<Vector2Int> OnLocationUpdated;

        string IDescribable.DisplayName => DisplayName;
        protected string DisplayName { get; set; }

        string IDescribable.Description => Description;
        protected string Description { get; set; }

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
                    OnLocationUpdated?.Invoke(newLocation);
                }
            }
        }

        List<IAttribute> IStatsCollection.Stats { get => Stats.Attributes; }
        IAttributeCollection Stats { get => stats as IAttributeCollection; }
        protected AttributeCollection stats = new AttributeCollection();

        IAttribute IStatsCollection.GetStat(string id)
        {
            return Stats.GetAttribute(id);
        }

    }
}

