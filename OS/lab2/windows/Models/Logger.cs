using System;
using System.IO;
using System.Threading;

using lab2.Interfaces;

namespace lab2.Models
{
    public sealed class Logger : ILogger
    {
        public Mutex Mutex { get; } = new Mutex();

        public void WriteMessage(string message)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logger.txt");
            File.AppendAllLines(path, new[] { message });
        }
    }
}