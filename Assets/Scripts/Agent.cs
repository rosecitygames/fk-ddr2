using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RCG
{
    public class Agent : MonoBehaviour, IDescribable, IAttributable, ILocatable
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
        List<AttributeData> attributeDatas = new List<AttributeData>();      
        List<IAttribute> IAttributable.Attributes
        {
            get
            {
                return Attributes;
            }
        }
        List<IAttribute> attributes = new List<IAttribute>();
        List<IAttribute> Attributes
        {
            get
            {
                if (attributes == null)
                {
                    attributes = new List<IAttribute>();
                    foreach (IAttribute attribute in attributeDatas)
                    {
                        attributes.Add(attribute);
                    }
                }
                return attributes;
            }
        }

        IAttribute IAttributable.GetAttribute(string id)
        {
            return GetAttribute(id);
        }
        IAttribute GetAttribute(string id)
        {
            return Attributes.Find(p => p.Id == id);
        }
        
        void IAttributable.AddAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute == false)
            {
                Attributes.Add(attribute);
            }        
        }

        void IAttributable.RemoveAttribute(IAttribute value)
        {
            if (value == null) return;

            IAttribute attribute = GetAttribute(value.Id);
            bool hasAttribute = attribute != null;
            if (hasAttribute)
            {
                Attributes.Remove(attribute);
            }
        }

        Vector2 ILocatable.Location
        {
            get
            {
                return transform.position; // TODO: Eventually maps to map grid
            }
        }

        // Has Desires

        // Has State Machine

        // Has Advertiser
    }
}
