﻿namespace IndieDevTools.Commands
{
    public interface ICommand
    {
        ICommandEnumerator Parent { get; set; }
        bool IsCompleted { get; }
        
        void Start();
        void Stop();
        void Destroy();
    }
}