using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG
{
    public interface IAttributable
    {
        List<IAttribute> Attributes { get; }
        void AddAttribute(IAttribute attribute);
        void RemoveAttribute(IAttribute attribute);
    }
}
