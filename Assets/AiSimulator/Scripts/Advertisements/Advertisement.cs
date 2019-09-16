using IndieDevTools.Traits;
using IndieDevTools.Maps;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public class Advertisement : IAdvertisement
    {
        protected ITraitCollection Attributes { get; set; }
        List<ITrait> ITraitCollection.Traits => Attributes.Traits;
        ITrait ITraitCollection.GetTrait(string id) => Attributes.GetTrait(id);
        void ITraitCollection.AddTrait(ITrait attribute) => Attributes.AddTrait(attribute);
        void ITraitCollection.RemoveTrait(ITrait attribute) => Attributes.RemoveTrait(attribute);
        void ITraitCollection.RemoveTrait(string id) => Attributes.RemoveTrait(id);
        void ITraitCollection.Clear() => Attributes.Clear();
        ITraitCollection ICopyable<ITraitCollection>.Copy() => Attributes.Copy();

        Vector2Int ILocatable.Location => Location;
        protected Vector2Int Location { get; set; }

        event Action<ILocatable> IUpdatable<ILocatable>.OnUpdated { add { } remove { } }

        List<Vector2Int> IAdvertisement.BroadcastLocations => BroadcastLocations;
        protected List<Vector2Int> BroadcastLocations { get; set; }

        IMap IAdvertisement.Map => Map;
        protected IMap Map { get; set; }

        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected int GroupId { get; set; }
  
        public static IAdvertisement Create(List<ITrait> attributes, IMap map, Vector2Int location, List<Vector2Int> broadcastLocations, int groupId = 0)
        {
            Advertisement advertisement = new Advertisement
            {
                Attributes = TraitCollection.Create(attributes),
                Map = map,
                Location = location,
                BroadcastLocations = broadcastLocations,
                GroupId = groupId
            };
            return advertisement;
        }
    }
}
