using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using lab2.Interfaces;

namespace lab2.Models
{
    public class Controller : IController, ICombinationsContainer
    {
        public event EventHandler AllThreadsCompleted;

        private IList<Thread> m_WorkThreads;
        private int m_CompletedThreadsCounter;

        private readonly ILogger m_Logger = new Logger();

        public ICollection<INumbersCombination> Combinations { get; private set; }
        public Mutex Mutex { get; } = new Mutex();

        public bool TryCreateThreads(string threadsCountInput)
        {
            int threadsCount;
            bool parseResult = int.TryParse(threadsCountInput, out threadsCount);
            if (!parseResult || threadsCount > 50 || threadsCount < 1) return false;

            m_WorkThreads = new List<Thread>();
            for (int i = 0; i < threadsCount; i++)
            {
                var worker = new Worker(m_Logger, this);
                var thread = new Thread(worker.StartCheckings)
                {
                    Name = i.ToString(),
                    IsBackground = true
                };

                m_WorkThreads.Add(thread);
                worker.WorkEnded += (sender, args) =>
                {
                    if (m_WorkThreads.Count == ++m_CompletedThreadsCounter)
                    {
                        AllThreadsCompleted?.Invoke(this, EventArgs.Empty);
                    }
                };
            }
            return true;
        }

        public void InitializeCombinations(int a, int b, int c)
        {
            Combinations = new List<INumbersCombination>
            {
                new NumbersCombination(a, b, c),
                new NumbersCombination(a, c, b),
                new NumbersCombination(b, a, c),
                new NumbersCombination(b, c, a),
                new NumbersCombination(c, a, b),
                new NumbersCombination(c, b, a)
            };
        }

        public void StartAllThreads()
        {            
            m_Logger.WriteMessage("------");
            
            if (m_WorkThreads == null || m_WorkThreads.Count == 0)
                throw new InvalidOperationException("Can't start operation without threads");

            if (Combinations == null || Combinations.Count == 0)
                throw new InvalidOperationException("Can't start operation without combinations");

            foreach (Thread thread in m_WorkThreads)
            {
                thread.Start();
            }
        }

        INumbersCombination ICombinationsContainer.TryGetCombination()
        {
            if (Combinations.Count == 0) return null;

            INumbersCombination combination = Combinations.FirstOrDefault();
            Combinations.Remove(combination);
            return combination;
        }
    }
}