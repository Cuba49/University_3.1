using System.Threading;

namespace lab2.Interfaces
{
    public interface ILogger
    {
        Mutex Mutex { get; }

        void WriteMessage(string message);
    }
}