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
        [NonSerialized]
        string runtimeDisplayName = "";
        string IDescribable.DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(runtimeDisplayName))
                {
                    runtimeDisplayName = displayName;
                }
                return runtimeDisplayName;
            }

            set
            {
                runtimeDisplayName = value;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        [NonSerialized]
        string runtimeDescription = "";
        string IDescribable.Description
        {
            get
            {
                if (string.IsNullOrEmpty(runtimeDescription))
                {
                    runtimeDescription = description;
                }
                return runtimeDescription;
            }

            set
            {
                runtimeDescription = value;
            }
        }

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
