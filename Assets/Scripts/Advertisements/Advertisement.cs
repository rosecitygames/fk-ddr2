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
        List<IAttribute> IAttributeCollection.Attributes { get { return AttributeCollection.Attributes; } }
        IAttribute IAttributeCollection.GetAttribute(string id) { return AttributeCollection.GetAttribute(id); }
        void IAttributeCollection.AddAttribute(IAttribute attribute) { AttributeCollection.AddAttribute(attribute); }
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) { AttributeCollection.RemoveAttribute(attribute); }
        void IAttributeCollection.Clear() { AttributeCollection.Clear(); }
        IAttributeCollection IAttributeCollection.Copy() { return AttributeCollection.Copy(); }

        protected Vector3Int Location { get; set; }
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

        protected int GroupId { get; set; }
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
 
        protected float BroadcastDistance { get; set; }
        float IAdvertisement.BroadcastDistance
        {
            get
            {
                return BroadcastDistance;
            }
        }

        public static IAdvertisement Create(List<IAttribute> attributes, Vector3Int location, float broadcastDistance, int groupId = 0)
        {
            Advertisement advertisement = new Advertisement
            {
                AttributeCollection = new AttributeCollection(attributes),
                Location = location,
                BroadcastDistance = broadcastDistance,
                GroupId = groupId
            };
            return advertisement;
        }
    }
}
