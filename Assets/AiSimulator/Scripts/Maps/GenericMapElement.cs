﻿using IndieDevTools.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Maps
{
    public class GenericMapElement : MonoBehaviour, IMapElement
    {
        IMapElement mapElement = null;
        IMapElement MapElement
        {
            get
            {
                InitMapElement();
                return mapElement;
            }
        }

        void InitMapElement()
        {
            if (mapElement == null)
            {
                mapElement = MapElementHelper.Create(gameObject, this);
                mapElement.AddToMap();
            }
        }

        int IGroupMember.GroupId => MapElement.GroupId;

        List<IAttribute> IStatsCollection.Stats => MapElement.Stats;
        IAttribute IStatsCollection.GetStat(string id) => MapElement.GetStat(id);

        string IDescribable.DisplayName => MapElement.DisplayName;
        string IDescribable.Description => MapElement.Description;

        IMap IMapElement.Map { get => MapElement.Map; set => MapElement.Map = value; }
        void IMapElement.AddToMap() => MapElement.AddToMap();
        void IMapElement.AddToMap(IMap map) => MapElement.AddToMap(map);
        void IMapElement.RemoveFromMap() => MapElement.RemoveFromMap();
        bool IMapElement.IsOnMap => MapElement.IsOnMap;
        float IMapElement.Distance(IMapElement otherMapElement) => MapElement.Distance(otherMapElement);
        float IMapElement.Distance(Vector2Int otherLocation) => MapElement.Distance(otherLocation);
        int IMapElement.InstanceId => MapElement.InstanceId;
        int IMapElement.SortingOrder =>  MapElement.SortingOrder;

        Vector2Int ILocatable.Location => MapElement.Location;
        event Action<Vector2Int> ILocatable.OnUpdated { add { MapElement.OnUpdated += value; } remove { MapElement.OnUpdated -= value; } }

        Vector3 IPositionable.Position { get => MapElement.Position; set => MapElement.Position = value; }
    }
}