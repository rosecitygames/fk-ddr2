namespace IndieDevTools.Attributes
{
    public interface IAttribute : ICopyable<IAttribute>, IUpdatable<IAttribute>, IDescribable, IIdable
    {
        int Quantity { get; set; }
        int Min { get; set; }
        int Max { get; set; }
        bool IsInitialMax { get; }
    }
}
