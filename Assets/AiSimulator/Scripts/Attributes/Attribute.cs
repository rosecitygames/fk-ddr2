using System;
using UnityEngine;

namespace IndieDevTools.Attributes
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

        string IIdable.Id => Data.Id;

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
                int newValue = Mathf.Clamp(value, Min, Max);
                if (quantity != newValue)
                {
                    quantity = newValue;
                    OnUpdated?.Invoke(this);
                }
            }
        }

        int initialQuantity;

        int IAttribute.Min { get => Min; set => Min = value; }
        int Min
        {
            get
            {
                if (isOverridingMin) return overrideMin;
                return Data.Min;
            }
            set
            {
                isOverridingMin = true;
                overrideMin = value;
            }
        }

        bool isOverridingMin = false;
        int overrideMin = 0;

        int IAttribute.Max { get => Max; set => Max = value; }
        int Max
        {
            get
            {
                if (isOverridingMax) return overrideMax;
                if (Data.IsInitialMax) return initialQuantity;
                return Data.Max;
            }
            set
            {
                isOverridingMax = true;
                overrideMax = value;
            }
        }

        bool isOverridingMax = false;
        int overrideMax = 0;

        bool IAttribute.IsInitialMax => Data.IsInitialMax;

        string IDescribable.DisplayName { get => Data.DisplayName; set => Data.DisplayName = value; }
        string IDescribable.Description { get => Data.Description; set => Data.Description = value; }
        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { (Data as IDescribable).OnUpdated += value; } remove { (Data as IDescribable).OnUpdated -= value; } }

        event Action<IAttribute> IUpdatable<IAttribute>.OnUpdated { add { OnUpdated += value; } remove { OnUpdated -= value; } }
        Action<IAttribute> OnUpdated;

        IAttribute ICopyable<IAttribute>.Copy()
        {
            IAttribute copy = new Attribute(this, quantity);
            return copy;
        }

        public Attribute (IAttribute source, int quantity = 0)
        {
            data = source;
            this.quantity = quantity;
            initialQuantity = quantity;
        }

        public Attribute() { }
    }
}
