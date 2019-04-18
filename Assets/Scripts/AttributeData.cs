using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    [System.Serializable]
    public class AttributeData : IAttribute
    {
        [SerializeField]
        Attribute attribute = null;
        IAttribute data;
        IAttribute Data
        {
            get
            {
                if (data == null)
                {
                    if (attribute == null)
                    {
                        //data = new NullAttribute();
                    }
                    else
                    {
                        data = attribute;
                    }
                }
                return data;
            }
        }

        string IAttribute.Id
        {
            get
            {
                return Data.Id;
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

        string IDescribable.DisplayName
        {
            get
            {
                return Data.DisplayName;
            }
        }

        string IDescribable.Description
        {
            get
            {
                return Data.Description;
            }
        }

        public AttributeData (IAttribute source, int quantity = 0)
        {
            data = source;
            this.quantity = quantity;
        }
    }
}
