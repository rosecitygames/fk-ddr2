using System;
using UnityEngine;

namespace IndieDevTools.Attributes
{

    [CreateAssetMenu(fileName = "Attribute", menuName = "RCG/Attribute")]
    public class ScriptableAttribute : ScriptableObject, IAttribute
    {
        [SerializeField]
        string id = "";
        string IIdable.Id => id;

        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName => displayName;

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description => description;

        [SerializeField]
        bool isInitialMax = false;
        bool IAttribute.IsInitialMax => isInitialMax;

        [SerializeField]
        int min = 0;
        int IAttribute.Min { get => min; set { } }

        [SerializeField]
        int max = 99;
        int IAttribute.Max { get => max; set { } }

        int IAttribute.Quantity { get => 0; set { } }

        event Action<IAttribute> IAttribute.OnUpdated { add { } remove { } }

        IAttribute IAttribute.Copy()
        {
            return new Attribute(this);
        }
    }
}
