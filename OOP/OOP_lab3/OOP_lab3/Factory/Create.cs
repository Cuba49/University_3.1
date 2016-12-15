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
        List<Cage> cages = new List<Cage>();
        public Create(Cage zoo)
        {
            Random r = new Random();
            int random = r.Next(1, 5);
            if (random == 3)
            {
                HaveGiraffe(zoo);
            }
            
            else if (random < 3)
            {
                HaveVolf(zoo);
            }
            else
            {
                HaveBear(zoo);
            }
        }

        void HaveBear(Cage zoo)
        {
            Console.WriteLine("Вам выпал медведь!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                Bear bear = new Bear(name, width);
                cages = zoo.NumberCage(bear, cages);
                int i = 1;
                Console.WriteLine("Куда хотите его поместить?");
                foreach (Cage cage in cages)
                {
                    Console.WriteLine("{0}){1}", i, cage.name);
                }
                int number = int.Parse(Console.ReadLine());
                cages[number - 1].Add(bear);
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveVolf(Cage zoo)
        {
            Console.WriteLine("Вам выпал волк!");
            
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                Wolf wolf = new Wolf(name, width);
                cages = zoo.NumberCage(wolf, cages);
                int i = 1;
                Console.WriteLine("Куда хотите его поместить?");
                foreach (Cage cage in cages)
                {
                    Console.WriteLine("{0}){1}", i, cage.name);
                }
                int number = int.Parse(Console.ReadLine());
                cages[number - 1].Add(wolf);
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveGiraffe(Cage zoo)
        {
            Console.WriteLine("Вам выпал жираф!");
            
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                Giraffe giraffe = new Giraffe(name, width);
                cages = zoo.NumberCage(giraffe, cages);
                int i = 1;
                Console.WriteLine("Куда хотите его поместить?");
                foreach (Cage cage in cages)
                {
                    Console.WriteLine("{0}){1}",i,cage.name);
                }
                int number = int.Parse(Console.ReadLine());
                cages[number-1].Add(giraffe);
                Console.Write("Животное успешно добавлено в зоопарк!");
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void NumberCage(Cage zoo, Animal animal)
        {
            cages=zoo.NumberCage(animal, cages);
        }
    }
}
