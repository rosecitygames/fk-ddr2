using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    public class Agent : MonoBehaviour, IDescribable, IAttributable, ILocatable
    {
        [SerializeField]
        string displayName;
        string IDescribable.DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        [SerializeField]
        string description;
        string IDescribable.Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        [SerializeField]
        List<Attribute> attributes = new List<Attribute>();
        List<IAttribute> IAttributable.Attributes
        {
            get
            {
                List<IAttribute> iattributes = new List<IAttribute>();
                foreach(IAttribute attribute in attributes)
                {
                    iattributes.Add(attribute);
                }
                return iattributes;
            }
        }

        void IAttributable.AddAttribute(IAttribute value)
        {
            // TODO: Check if id exists -- see twnd linq
           attributes.Add(Attribute.Create(value));
        }

        void IAttributable.RemoveAttribute(IAttribute value)
        {
            foreach(Attribute attribute in attributes)
            {
                if ((attribute as IAttribute).Id == value.Id)
                {
                    attributes.Remove(attribute);
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
