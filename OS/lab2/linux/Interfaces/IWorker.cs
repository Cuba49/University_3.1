using System;

namespace lab2.Interfaces
{
    public interface IWorker
    {
        event EventHandler WorkEnded;

        ILogger Logger { get; }
        ICombinationsContainer Container { get; }

        /// <summary>
        /// Starts take combinations from container
        /// </summary>
        void StartCheckings();
    }
}
