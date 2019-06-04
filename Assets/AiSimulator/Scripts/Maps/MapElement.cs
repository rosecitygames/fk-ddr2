using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Maps
{
    public class MapElement : IMapElement
    {

        IMap IMapElement.Map { get => Map; set => Map = value; }
        protected IMap Map { get; set; }

        void IMapElement.AddToMap(IMap map)
        {
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap()
        {
            Map.RemoveElement(this);
        }

        float IMapElement.Distance(IMapElement otherMapElement)
        {
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        int IMapElement.InstanceId => InstanceId;
        protected int InstanceId { get; }

        int IMapElement.SortingOrder { get => SortingOrder; }
        protected int SortingOrder { get; }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected int GroupId { get; set; }

        Vector3Int ILocatable.Location { get => Location; }
        protected Vector3Int Location { get; set; }

        string IDescribable.DisplayName { get => DisplayName; }
        protected string DisplayName { get; set; }

        string IDescribable.Description { get => Description; }
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
                Vector3Int newLocation = Map.LocalToCell(value);
                if (Location != newLocation)
                {
                    Map.AddElement(this);
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

