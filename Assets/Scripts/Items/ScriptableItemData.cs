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
        AttributeCollection attributes = new AttributeCollection();
        IAttributeCollection attributeCollection { get { return attributes as IAttributeCollection; } }
        List<IAttribute> IAttributeCollection.Attributes { get { return attributeCollection.Attributes; } }
        IAttribute IAttributeCollection.GetAttribute(string id) { return attributeCollection.GetAttribute(id); }
        void IAttributeCollection.AddAttribute(IAttribute attribute) { attributeCollection.AddAttribute(attribute); }
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) { attributeCollection.RemoveAttribute(attribute); }
        void IAttributeCollection.Clear() { attributeCollection.Clear(); }
        IAttributeCollection IAttributeCollection.Copy() { return attributeCollection.Copy(); }

        IItemData IItemData.Copy()
        {
            return ItemData.Create(this);
        }
    }
}

