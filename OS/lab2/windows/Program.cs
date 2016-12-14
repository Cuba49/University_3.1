using System;

using lab2.Models;

namespace lab2
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var menu = new Menu();
            menu.StartDialog();
            Console.ReadKey(false);
        }
    }
}