namespace RCG
{
    public interface IAttribute : IDescribable
    {
        string Id { get; set; }
        int Quantity { get; set; }
    }
}
