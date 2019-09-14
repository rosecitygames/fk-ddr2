using IndieDevTools.Common;
using System;

namespace IndieDevTools.Attributes
{
    public interface IAttribute : ICopyable<IAttribute>, IDescribable, IIdable
    {
        int Quantity { get; set; }
        int Min { get; set; }
        int Max { get; set; }
        bool IsInitialMax { get; }
        event Action<IAttribute> OnUpdated;
    }
}
