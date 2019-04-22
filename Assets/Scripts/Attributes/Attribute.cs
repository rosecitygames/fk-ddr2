using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Attributes
{
    [System.Serializable]
    public class Attribute : IAttribute
    {
        [SerializeField]
        ScriptableAttribute attribute = null;
        IAttribute data;
        IAttribute Data
        {
            get
            {
                InitData();
                return data;
            }
        }

        void InitData()
        {
            if (data == null)
            {
                if (attribute == null)
                {
                    data = new NullAttribute();
                }
                else
                {
                    data = attribute;
                }
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

        IAttribute IAttribute.Copy()
        {
            IAttribute copy = new Attribute(this, quantity);
            return copy;
        }

        public Attribute (IAttribute source, int quantity = 0)
        {
            data = source;
            this.quantity = quantity;
        }

        public Attribute() { }
    }
}
