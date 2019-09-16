using System;
using UnityEngine;

namespace IndieDevTools.Traits
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "Trait", menuName = "IndieDevTools/Trait")]
    public class ScriptableTrait : ScriptableObject, ITrait
    {
        string IIdable.Id => name;

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
                if (runtimeDisplayName != value)
                {
                    runtimeDisplayName = value;
                    OnDescribableUpdated.Invoke(this);
                }
            }
        }

        [SerializeField, TextArea]
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
                if (runtimeDescription != value)
                {
                    runtimeDescription = value;
                    OnDescribableUpdated.Invoke(this);
                }
            }
        }

        event Action<IDescribable> IUpdatable<IDescribable>.OnUpdated { add { OnDescribableUpdated += value; } remove { OnDescribableUpdated -= value; } }
        Action<IDescribable> OnDescribableUpdated;

        [SerializeField]
        bool isInitialMax = false;
        bool ITrait.IsInitialMax => isInitialMax;

        [SerializeField]
        int min = 0;
        int ITrait.Min { get => min; set { } }

        [SerializeField]
        int max = 99;
        int ITrait.Max { get => max; set { } }

        int ITrait.Quantity { get => 0; set { } }

        event Action<ITrait> IUpdatable<ITrait>.OnUpdated { add { } remove { } }

        ITrait ICopyable<ITrait>.Copy()
        {
            return new Trait(this);
        }
    }
}
