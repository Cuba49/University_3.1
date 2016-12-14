using System;

using lab2.Interfaces;

namespace lab2.Models
{
    public class Menu
    {
        private readonly IController m_Controller;

        public Menu()
        {
            m_Controller = new Controller();
        }

        public void StartDialog()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Laboratory work 2 (Dusaniuk Yaroslav 3cs-a2)");
                Console.Write("Write numbers of threads (number between 1 and 50): ");

                bool result = m_Controller.TryCreateThreads(Console.ReadLine());
                if (result) break;

                Console.Clear();
                Console.WriteLine("Invalid input. Try again.");
                Console.ReadKey(false);
            }

            var rnd = new Random();
            int[] rndNumbers =
            {
                rnd.Next(0, 10),
                rnd.Next(0, 10),
                rnd.Next(0, 10)
            };
            m_Controller.InitializeCombinations(rndNumbers[0], rndNumbers[1], rndNumbers[2]);
            Console.WriteLine($"Generated numbers: {rndNumbers[0]}, {rndNumbers[1]}, {rndNumbers[2]}");

            m_Controller.AllThreadsCompleted += (sender, args) => Console.WriteLine("All thread finished their work");

            m_Controller.StartAllThreads();
        }
    }
}