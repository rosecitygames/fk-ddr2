using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Attributes;
using IndieDevTools.Maps;
using System;

namespace IndieDevTools.Advertisements
{
    public class Advertisement : IAdvertisement
    {
        protected IAttributeCollection Attributes { get; set; }
        List<IAttribute> IAttributeCollection.Attributes => Attributes.Attributes;
        IAttribute IAttributeCollection.GetAttribute(string id) => Attributes.GetAttribute(id);
        void IAttributeCollection.AddAttribute(IAttribute attribute) => Attributes.AddAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) => Attributes.RemoveAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(string id) => Attributes.RemoveAttribute(id);
        void IAttributeCollection.Clear() => Attributes.Clear();
        IAttributeCollection IAttributeCollection.Copy() => Attributes.Copy();

        Vector2Int ILocatable.Location => Location;
        protected Vector2Int Location { get; set; }

        event Action<Vector2Int> ILocatable.OnUpdated { add { } remove { } }

        List<Vector2Int> IAdvertisement.BroadcastLocations => BroadcastLocations;
        protected List<Vector2Int> BroadcastLocations { get; set; }

        IMap IAdvertisement.Map => Map;
        protected IMap Map { get; set; }

        int IGroupMember.GroupId => GroupId;
        protected int GroupId { get; set; }
  
        public static IAdvertisement Create(List<IAttribute> attributes, IMap map, Vector2Int location, List<Vector2Int> broadcastLocations, int groupId = 0)
        {
            Advertisement advertisement = new Advertisement
            {
                Attributes = AttributeCollection.Create(attributes),
                Map = map,
                Location = location,
                BroadcastLocations = broadcastLocations,
                GroupId = groupId
            };
            return advertisement;
        }
    }
}
