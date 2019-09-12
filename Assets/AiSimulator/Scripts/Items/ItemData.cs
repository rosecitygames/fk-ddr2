using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Advertisements;
using RCG.Attributes;

namespace RCG.Items
{
    [System.Serializable]
    public class ItemData : IItemData
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description
        {
            get
            {
                return description;
            }
        }

        [SerializeField]
        float broadcastDistance = 0.0f;
        float IAdvertisementBroadcastData.BroadcastDistance
        {
            get
            {
                return broadcastDistance;
            }
        }

        [SerializeField]
        float broadcastInterval = 0.0f;
        float IAdvertisementBroadcastData.BroadcastInterval
        {
            get
            {
                return broadcastInterval;
            }
        }

        [SerializeField]
        AttributeCollection stats = new AttributeCollection();
        IAttributeCollection iStats = null;
        IAttributeCollection Stats
        {
            get
            {
                if (iStats == null)
                {
                    iStats = stats;
                }
                return iStats;
            }
        }
        List<IAttribute> IStatsCollection.Stats { get => Stats.Attributes; }
        IAttribute IStatsCollection.GetStat(string id) { return Stats.GetAttribute(id); }

        IItemData IItemData.Copy()
        {
            return Create(this);
        }

        public static IItemData Create(IItemData source)
        {
            if (source == null)
            {
                return new ItemData();
            }
            return new ItemData(source);
        }

        public static IItemData Create()
        {
            return new ItemData();
        }

        public ItemData(IItemData source)
        {
            displayName = source.DisplayName;
            description = source.Description;
            iStats = AttributeCollection.Create(source.Stats);
            broadcastDistance = source.BroadcastDistance;
            broadcastInterval = source.BroadcastInterval;
        }

        public ItemData() { }
    }
}