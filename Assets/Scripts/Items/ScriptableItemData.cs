using RCG.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "RCG/Item Data")]
    public class ScriptableItemData : ScriptableObject, IItemData
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
            return ItemData.Create(this);
        }
    }
}

