namespace RCG
{
    public interface IInventoryItem : IDescribable
    {
        string Id { get; set; }
        int Quantity { get; set; }
    }
}
