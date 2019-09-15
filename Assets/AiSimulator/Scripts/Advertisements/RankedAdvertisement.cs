using IndieDevTools.Attributes;
using IndieDevTools.Maps;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public class RankedAdvertisement : IRankedAdvertisement
    {
        protected IAdvertisement Advertisement { get; set; }
        public int Rank { get; set; }

        List<IAttribute> IAttributeCollection.Attributes => Advertisement.Attributes;
        IAttribute IAttributeCollection.GetAttribute(string id) => Advertisement.GetAttribute(id);
        void IAttributeCollection.AddAttribute(IAttribute attribute) => Advertisement.AddAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) => Advertisement.RemoveAttribute(attribute);
        void IAttributeCollection.RemoveAttribute(string id) => Advertisement.RemoveAttribute(id);
        void IAttributeCollection.Clear() => Advertisement.Clear();
        IAttributeCollection ICopyable<IAttributeCollection>.Copy() => Advertisement.Copy();

        IMap IAdvertisement.Map => Advertisement.Map;
        List<Vector2Int> IAdvertisement.BroadcastLocations => Advertisement.BroadcastLocations;

        Vector2Int ILocatable.Location => Advertisement.Location;
        event Action<ILocatable> IUpdatable<ILocatable>.OnUpdated { add { } remove { } }

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

