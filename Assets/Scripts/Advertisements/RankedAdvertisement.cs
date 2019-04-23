using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Advertisements
{
    public class RankedAdvertisement : IRankedAdvertisement
    {
        protected IAdvertisement Advertisement { get; set; }
        public int Rank { get; set; }

        List<IAttribute> IAttributeCollection.Attributes { get { return Advertisement.Attributes; } }
        IAttribute IAttributeCollection.GetAttribute(string id) { return Advertisement.GetAttribute(id); }
        void IAttributeCollection.AddAttribute(IAttribute attribute) { Advertisement.AddAttribute(attribute); }
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) { Advertisement.RemoveAttribute(attribute); }
        void IAttributeCollection.Clear() { Advertisement.Clear(); }
        IAttributeCollection IAttributeCollection.Copy() { return Advertisement.Copy(); }
        Vector2 ILocatable.Location { get{ return Advertisement.Location; } }
        float IAdvertisement.BroadcastDistance { get { return Advertisement.BroadcastDistance; } }

        public static RankedAdvertisement Create(IAdvertisement advertisement, int rank)
        {
            return new RankedAdvertisement
            {
                Advertisement = advertisement,
                Rank = rank
            };
        }
    }
}

