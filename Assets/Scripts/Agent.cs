using System.Collections;
using System.Collections.Generic;
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
        
        void IAttributable.AddAttribute(IAttribute attribute)
        {
            Attributes.Add(attribute);
        }

        void IAttributable.RemoveAttribute(IAttribute value)
        {
            foreach(IAttribute attribute in Attributes)
            {
                if (attribute.Id == value.Id)
                {
                    Attributes.Remove(attribute);
                    break;
                }
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
