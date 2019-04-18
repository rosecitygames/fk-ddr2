using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    [System.Serializable]
    public class NullAttribute : IAttribute
    {
        string IAttribute.Id
        {
            get
            {
                return "";
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

        string IDescribable.DisplayName
        {
            get
            {
                return "";
            }
        }

        string IDescribable.Description
        {
            get
            {
                return "";
            }
        }

    }
}
