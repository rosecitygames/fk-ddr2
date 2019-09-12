namespace RCG.Attributes
{
    public interface ICounter
    {
        int Count { get; set; }
        string Prefix { get; set; }
        string Suffix { get; set; }
    }
}