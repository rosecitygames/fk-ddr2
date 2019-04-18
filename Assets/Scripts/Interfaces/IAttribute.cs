namespace RCG
{
    interface IAttribute : IDescribable
    {
        string Id { get; set; }
        int Value { get; set; }
    }
}
