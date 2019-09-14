using System;

namespace IndieDevTools.Attributes
{
    [System.Serializable]
    public class NullAttribute : IAttribute
    {
        string IDescribable.DisplayName { get => ""; set { } }
        string IDescribable.Description { get => ""; set { } }
        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { } remove { } }

        string IIdable.Id { get => ""; }
        int IAttribute.Quantity { get => 0; set { } }
        int IAttribute.Min { get => 0; set { } }
        int IAttribute.Max { get => 0; set { } }
        bool IAttribute.IsInitialMax => false;

        event Action<IAttribute> IUpdatable<IAttribute>.OnUpdated { add { } remove { } }

        IAttribute ICopyable<IAttribute>.Copy() => new NullAttribute();

        public static IAttribute Create() => new NullAttribute();
    }
}
