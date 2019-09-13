using System;

namespace IndieDevTools.Attributes
{
    public interface IAttribute : IDescribable, IIdable
    {
        int Quantity { get; set; }
        int Min { get; set; }
        int Max { get; set; }
        bool IsInitialMax { get; }
        event Action<IAttribute> OnUpdated;
        IAttribute Copy();
    }
}
