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
                if (runtimeDisplayName != value)
                {
                    runtimeDisplayName = value;
                    OnDescribableUpdated.Invoke(this);
                }
            }
        }

        [SerializeField, TextArea]
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
                if (runtimeDescription != value)
                {
                    runtimeDescription = value;
                    OnDescribableUpdated.Invoke(this);
                }
            }
        }

        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { OnDescribableUpdated += value; } remove { OnDescribableUpdated -= value; } }
        Action<IDescribable> OnDescribableUpdated;

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

        IItemData ICopyable<IItemData>.Copy()
        {
            return ItemData.Create(this);
        }
    }
}
