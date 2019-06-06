using RCG.Attributes;
using RCG.Maps;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public class RankedAdvertisement : IRankedAdvertisement
    {
        protected IAdvertisement Advertisement { get; set; }
        public int Rank { get; set; }

        List<IAttribute> IAttributeCollection.Attributes => Advertisement.Attributes;
        IAttribute IAttributeCollection.GetAttribute(string id) => Advertisement.GetAttribute(id);
        void IAttributeCollection.AddAttribute(IAttribute attribute) => Advertisement.AddAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) => Advertisement.RemoveAttribute(attribute);
        void IAttributeCollection.Clear() => Advertisement.Clear();
        IAttributeCollection IAttributeCollection.Copy() => Advertisement.Copy();
        IMap IAdvertisement.Map => Advertisement.Map;
        List<Vector3Int> IAdvertisement.BroadcastLocations => Advertisement.BroadcastLocations;
        Vector3Int ILocatable.Location => Advertisement.Location;
        int IGroupMember.GroupId { get => Advertisement.GroupId; set => Advertisement.GroupId = value; }

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

