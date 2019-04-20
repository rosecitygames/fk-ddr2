using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    [System.Serializable]
    public class AttributeCollection : IAttributeCollection
    {
        [SerializeField]
        List<Attribute> attributes = new List<Attribute>();
        List<IAttribute> IAttributeCollection.Attributes
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

        IAttribute IAttributeCollection.GetAttribute(string id)
        {
            return GetAttribute(id);
        }
        IAttribute GetAttribute(string id)
        {
            return Collection.Find(attribute => attribute.Id == id);
        }

        void IAttributeCollection.AddAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute == false)
            {
                Collection.Add(attribute);
            }
        }

        void IAttributeCollection.RemoveAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute)
            {
                Collection.Remove(attribute);
            }
        }

        void IAttributeCollection.Clear()
        {
            Collection.Clear();
        }

        IAttributeCollection IAttributeCollection.Copy()
        {
            IAttributeCollection copy = new AttributeCollection();
            foreach(IAttribute attribute in Collection)
            {
                copy.AddAttribute(attribute.Copy());
            }
            return copy;
        }

        public AttributeCollection(IAttributeCollection source)
        {
            foreach (IAttribute attribute in source.Attributes)
            {
                Collection.Add(attribute.Copy());
            }
        }

        public AttributeCollection(List<IAttribute> source)
        {
            foreach (IAttribute attribute in source)
            {
                Collection.Add(attribute.Copy());
            }
        }

        public AttributeCollection() { }

    }
}
