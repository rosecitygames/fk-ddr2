using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    public class Attribute : IAttribute
    {
        [SerializeField]
        string id;
        string IAttribute.Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [SerializeField]
        int quantity;
        int IAttribute.Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

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

        public static Attribute Create(IAttribute source)
        {
            Attribute attribute = new Attribute();
            attribute.id = source.Id;
            attribute.quantity = source.Quantity;
            attribute.displayName = source.DisplayName;
            attribute.description = source.Description;
            return attribute;
        }
    }
}
