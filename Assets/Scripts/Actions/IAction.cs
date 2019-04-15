namespace RCG.Actions
{
    public interface IAction
    {
        IActionEnumerator Parent { get; set; }
        bool IsCompleted { get; }
        
        void Start();
        void Stop();
        void Destroy();
    }
}