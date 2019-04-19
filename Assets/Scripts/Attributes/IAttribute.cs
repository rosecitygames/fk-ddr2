namespace RCG
{
    public interface IAttribute : IDescribable
    {
        string Id { get;}
        int Quantity { get; set; }
        IAttribute Copy();
    }
}
