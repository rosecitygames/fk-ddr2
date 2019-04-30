using System.Collections.Generic;

namespace RCG.Attributes
{
    public interface IAttributeCollection
    {
        List<IAttribute> Attributes { get; }
        IAttribute GetAttribute(string id);

        void AddAttribute(IAttribute attribute);
        void RemoveAttribute(IAttribute attribute);
        void Clear();

        IAttributeCollection Copy();
    }
}
