using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    [System.Serializable]
    public class AttributeCollection : IAttributable
    {
        [SerializeField]
        List<AttributeData> attributes = new List<AttributeData>();
        List<IAttribute> IAttributable.Attributes
        {
            get
            {
                return Collection;
            }
        }
        List<IAttribute> collection = new List<IAttribute>();
        List<IAttribute> Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = new List<IAttribute>();
                    foreach (IAttribute attribute in attributes)
                    {
                        collection.Add(attribute);
                    }
                }
                return collection;
            }
        }

        IAttribute IAttributable.GetAttribute(string id)
        {
            IAttribute attribute = GetAttribute(id);
            if (attribute == null)
            {
                return new NullAttribute();
            }
            return attribute;
        }
        IAttribute GetAttribute(string id)
        {
            return Collection.Find(p => p.Id == id);
        }

        void IAttributable.AddAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute == false)
            {
                Collection.Add(attribute);
            }
        }

        void IAttributable.RemoveAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute)
            {
                Collection.Remove(attribute);
            }
        }
    }
}
