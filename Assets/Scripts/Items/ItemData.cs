using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        AttributeCollection stats = new AttributeCollection();
        IStatsCollection statsCollection { get { return stats as IStatsCollection; } }
        List<IAttribute> IStatsCollection.Stats { get { return statsCollection.Stats; } }
        IAttribute IStatsCollection.GetStat(string id) { return statsCollection.GetStat(id); }

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
            stats = new AttributeCollection(source.Stats);
        }

        public ItemData() { }
    }
}
