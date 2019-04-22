using RCG.Attributes;
using System.Collections.Generic;

namespace RCG.Items
{
    public class NullItemData : IItemData
    {
        string IDescribable.DisplayName { get { return ""; } }
        string IDescribable.Description {  get { return ""; } }

        AttributeCollection attributes = new AttributeCollection();
        IAttributeCollection attributeCollection { get { return attributes as IAttributeCollection; } }
        List<IAttribute> IAttributeCollection.Attributes { get { return attributeCollection.Attributes; } }
        IAttribute IAttributeCollection.GetAttribute(string id) { return attributeCollection.GetAttribute(id); }
        void IAttributeCollection.AddAttribute(IAttribute attribute) { }
        void IAttributeCollection.RemoveAttribute(IAttribute attribute) { }
        void IAttributeCollection.Clear() { }
        IAttributeCollection IAttributeCollection.Copy() { return attributeCollection.Copy(); }

        IItemData IItemData.Copy() { return new NullItemData(); }
    }
}

