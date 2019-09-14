using System.Collections.Generic;

namespace IndieDevTools.Attributes
{
    public interface IAttributeCollection : ICopyable<IAttributeCollection>
    {
        List<IAttribute> Attributes { get; }
        IAttribute GetAttribute(string id);

        void AddAttribute(IAttribute attribute);
        void RemoveAttribute(IAttribute attribute);
        void RemoveAttribute(string id);
        void Clear();
    }
}
