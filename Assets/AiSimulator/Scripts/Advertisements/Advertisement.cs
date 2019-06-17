using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using RCG.Maps;

namespace RCG.Advertisements
{
    public class Advertisement : IAdvertisement
    {
        protected IAttributeCollection AttributeCollection { get; set; }
        List<IAttribute> IAttributeCollection.Attributes => AttributeCollection.Attributes;
        IAttribute IAttributeCollection.GetAttribute(string id) => AttributeCollection.GetAttribute(id);
        void IAttributeCollection.AddAttribute(IAttribute attribute) => AttributeCollection.AddAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) => AttributeCollection.RemoveAttribute(attribute);
        void IAttributeCollection.Clear() => AttributeCollection.Clear();
        IAttributeCollection IAttributeCollection.Copy() => AttributeCollection.Copy();

        Vector2Int ILocatable.Location => Location;
        protected Vector2Int Location { get; set; }

        List<Vector2Int> IAdvertisement.BroadcastLocations => BroadcastLocations;
        protected List<Vector2Int> BroadcastLocations { get; set; }

        IMap IAdvertisement.Map => Map;
        protected IMap Map { get; set; }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected int GroupId { get; set; }
  
        public static IAdvertisement Create(List<IAttribute> attributes, IMap map, Vector2Int location, List<Vector2Int> broadcastLocations, int groupId = 0)
        {
            Advertisement advertisement = new Advertisement
            {
                AttributeCollection = new AttributeCollection(attributes),
                Map = map,
                Location = location,
                BroadcastLocations = broadcastLocations,
                GroupId = groupId
            };
            return advertisement;
        }
    }
}
