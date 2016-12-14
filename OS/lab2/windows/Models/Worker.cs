using System;
using System.Threading;

using lab2.Interfaces;

namespace lab2.Models
{
    public class Worker : IWorker
    {
        public event EventHandler WorkEnded;

        public Worker(ILogger logger, ICombinationsContainer container)
        {
            Logger = logger;
            Container = container;
        }

        public ILogger Logger { get; }
        public ICombinationsContainer Container { get; }

        public void StartCheckings()
        {
            while (true)
            {
                Container.Mutex.WaitOne();
                INumbersCombination combination = Container.TryGetCombination();
                Container.Mutex.ReleaseMutex();

                if (combination == null) break;

                string checkingResult = CheckCombination(combination);
                checkingResult += $" (thread: {Thread.CurrentThread.Name})";

                Logger.Mutex.WaitOne();
                Logger.WriteMessage(checkingResult);
                Logger.Mutex.ReleaseMutex();
            }

            WorkEnded?.Invoke(this, EventArgs.Empty);
        }

        private static string CheckCombination(INumbersCombination combination)
        {
            string result = combination.A * combination.B == combination.C + combination.D 
                ? $"{combination.A} * {combination.B} == {combination.C} + {combination.D}" 
                : $"{combination.A} * {combination.B} != {combination.C} + {combination.D}";

            return result;
        }
    }
}