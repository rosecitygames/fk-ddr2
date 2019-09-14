namespace IndieDevTools
{
    public interface IDescribable : IUpdatable<IDescribable>
    {
        string DisplayName { get; set; }
        string Description { get; set; }
    }
}