using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{

    [CreateAssetMenu(fileName = "Attribute", menuName = "RCG/Attribute")]
    public class Attribute : ScriptableObject, IAttribute
    {
        [SerializeField]
        string id = "";
        string IAttribute.Id
        {
            get
            {
                return id;
            }
        }

        int IAttribute.Quantity
        {
            get
            {
                return 0;
            }
            set { }
        }

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
        [TextArea]
        string description = "";
        string IDescribable.Description
        {
            get
            {
                return description;
            }
        }
    }
}
