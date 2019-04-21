using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public class Advertisement : IAdvertisement
    {
        protected IAttributeCollection attributeCollection = null;
        List<IAttribute> IAttributeCollection.Attributes { get { return attributeCollection.Attributes; } }
        IAttribute IAttributeCollection.GetAttribute(string id) { return attributeCollection.GetAttribute(id); }
        void IAttributeCollection.AddAttribute(IAttribute attribute) { attributeCollection.AddAttribute(attribute); }
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) { attributeCollection.RemoveAttribute(attribute); }
        void IAttributeCollection.Clear() { attributeCollection.Clear(); }
        IAttributeCollection IAttributeCollection.Copy() { return attributeCollection.Copy(); }

        protected Vector2 location;
        Vector2 ILocatable.Location
        {
            get
            {
                return location;
            }          
        }

        protected float broadcastDistance;
        float IAdvertisement.BroadcastDistance
        {
            get
            {
                return broadcastDistance;
            }
        }

        public static IAdvertisement Create(List<IAttribute> attributes, Vector2 location, float broadcastDistance)
        {
            Advertisement advertisement = new Advertisement
            {
                attributeCollection = new AttributeCollection(attributes),
                location = location,
                broadcastDistance = broadcastDistance
            };

            return advertisement;
        }
    }
}
