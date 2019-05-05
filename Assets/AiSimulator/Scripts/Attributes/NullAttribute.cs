using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Attributes
{
    [System.Serializable]
    public class NullAttribute : IAttribute
    {
        string IAttribute.Id { get => ""; }
        int IAttribute.Quantity { get => 0; set { } }

        IAttribute IAttribute.Copy() { return new NullAttribute(); }

        string IDescribable.DisplayName { get => ""; }
        string IDescribable.Description { get => ""; }
    }
}
