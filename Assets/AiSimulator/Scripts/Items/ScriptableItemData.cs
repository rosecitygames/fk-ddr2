using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "RCG/Item Data")]
    public class ScriptableItemData : ScriptableObject, IItemData
    {
        [SerializeField]
        string displayName = "";
        [NonSerialized]
        string runtimeDisplayName = "";
        string IDescribable.DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(runtimeDisplayName))
                {
                    runtimeDisplayName = displayName;
                }
                return runtimeDisplayName;
            }

            set
            {
                runtimeDisplayName = value;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        [NonSerialized]
        string runtimeDescription = "";
        string IDescribable.Description
        {
            get
            {
                if (string.IsNullOrEmpty(runtimeDescription))
                {
                    runtimeDescription = description;
                }
                return runtimeDescription;
            }

            set
            {
                runtimeDescription = value;
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
        IAttributeCollection Stats { get => stats as IAttributeCollection; }
        List<IAttribute> IStatsCollection.Stats { get => Stats.Attributes; }
        IAttribute IStatsCollection.GetStat(string id) { return Stats.GetAttribute(id); }

        IItemData IItemData.Copy()
        {
            return ItemData.Create(this);
        }
    }
}
