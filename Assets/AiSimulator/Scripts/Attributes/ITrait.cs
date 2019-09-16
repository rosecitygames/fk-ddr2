namespace IndieDevTools.Traits
{
    public interface ITrait : ICopyable<ITrait>, IUpdatable<ITrait>, IDescribable, IIdable
    {
        int Quantity { get; set; }
        int Min { get; set; }
        int Max { get; set; }
        bool IsInitialMax { get; }
    }
}
