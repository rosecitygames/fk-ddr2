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
            attributes = new AttributeCollection(source.Attributes);
        }

        public ItemData() { }
    }
}
