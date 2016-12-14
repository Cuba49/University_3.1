using System;

using Client.TimeService;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITimeService service = new TimeServiceClient();
            Console.WriteLine($"Current server time: {service.GetCurrentTime()}");

            Console.WriteLine("\nClick any button to continue...");
            Console.ReadKey(false);
        }
    }
}