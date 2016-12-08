using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_lab3.Animals;

namespace OOP_lab3.Factory
{
    class Create
    {
        public Create(Cage giraffes, Cage bears, Cage wolfs)
        {
            CreateAnimal(giraffes, bears, wolfs);
        }

        public void CreateAnimal(Cage giraffes, Cage bears, Cage wolfs)
        {
            Random r = new Random();
            int random = r.Next(1, 5);
            if (random == 3)
            {
                HaveGiraffe(giraffes);
            }
            if (random < 3)
            {
                 HaveVolf(wolfs);
            }
            else
            {
                HaveBear(bears);
            }
        }
        void HaveBear(Cage bears)
        {
            Console.WriteLine("Вам выпал медведь!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                bears.Add(new Bear(name, width));
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveVolf(Cage wolfs)
        {
            Console.WriteLine("Вам выпал волк!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                wolfs.Add(new Wolf(name, width));
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveGiraffe(Cage giraffes)
        {
            Console.WriteLine("Вам выпал жираф!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                giraffes.Add(new Giraffe(name, width));
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }
    }
}
