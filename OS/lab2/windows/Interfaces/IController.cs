using System;

namespace lab2.Interfaces
{
    public interface IController
    {
        event EventHandler AllThreadsCompleted;

        void InitializeCombinations(int a, int b, int c, int d);
        bool TryCreateThreads(string threadsCountInput);
        void StartAllThreads();
    }
}
