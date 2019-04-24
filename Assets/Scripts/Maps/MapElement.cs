using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Maps
{
    public class MapElement : IMapElement
    {

        IMap IMapElement.Map
        {
            get
            {
                return Map;
            }
            set
            {
                Map = value;
            }
        }
        IMap Map { get; set; }

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

        int IGroupMember.GroupId
        {
            get
            {
                return GroupId;
            }
            set
            {
                GroupId = value;
            }
        }
        protected int GroupId { get; set; }

        Vector3Int ILocatable.Location
        {
            get
            {
                return Location;
            }
            set
            {
                Location = value;
            }
        }
        protected Vector3Int Location { get; set; }

        string IDescribable.DisplayName
        {
            get
            {
                return DisplayName;
            }
        }
        protected string DisplayName { get; set; }


        string IDescribable.Description
        {
            get
            {
                return Description;
            }
        }
        protected string Description { get; set; }

        List<IAttribute> IStatsCollection.Stats
        {
            get
            {
                return Stats.Attributes;
            }
        }
        IAttributeCollection Stats
        {
            get
            {
                return stats as IAttributeCollection;
            }
        }
        protected AttributeCollection stats = new AttributeCollection();

        IAttribute IStatsCollection.GetStat(string id)
        {
            return Stats.GetAttribute(id);
        }

    }
}

