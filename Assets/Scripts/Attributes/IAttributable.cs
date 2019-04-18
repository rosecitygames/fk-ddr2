using System.Collections.Generic;

namespace RCG
{
    public interface IAttributable
    {
        List<IAttribute> Attributes { get; }
        IAttribute GetAttribute(string id);

        void AddAttribute(IAttribute attribute);
        void RemoveAttribute(IAttribute attribute);       
    }
}
