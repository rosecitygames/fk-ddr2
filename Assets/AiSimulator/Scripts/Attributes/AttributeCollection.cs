﻿using System.Collections.Generic;
using UnityEngine;
namespace IndieDevTools.Attributes
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
        List<IAttribute> collection = null;
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
            RemoveAttribute(value.Id);
        }

        void IAttributeCollection.RemoveAttribute(string id) => RemoveAttribute(id);
        void RemoveAttribute(string id)
        {
            IAttribute attribute = GetAttribute(id);
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

        public static IAttributeCollection Create(IAttributeCollection source)
        {
            AttributeCollection attributeCollection = new AttributeCollection();
            foreach (IAttribute attribute in source.Attributes)
            {
                attributeCollection.Collection.Add(attribute.Copy());
            }
            return attributeCollection;
        }

        public static IAttributeCollection Create(List<IAttribute> source)
        {
            AttributeCollection attributeCollection = new AttributeCollection();
            foreach (IAttribute attribute in source)
            {
                attributeCollection.Collection.Add(attribute.Copy());
            }
            return attributeCollection;
        }

        public static IAttributeCollection Create()
        {
            return new AttributeCollection();
        }
    }
}
