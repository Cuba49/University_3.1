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
        private string name;
        private int width;
        private Animal thisAnimal;
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
            ForAll();
            try
            {
                thisAnimal = new Bear(name, width);
                Add(zoo);
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

            ForAll();
            try
            {
                thisAnimal = new Wolf(name, width);
                Add(zoo);
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
            ForAll();
            try
            {
                thisAnimal = new Giraffe(name, width);
                Add(zoo);
            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void ForAll()
        {
            Console.Write("Введите имя:");
            name = Console.ReadLine();
            Console.Write("Введите вес:");
            width = int.Parse(Console.ReadLine());
        }

        void Add(Cage zoo)
        {
            try
            {
                cages = zoo.NumberCage(thisAnimal, cages);
                int i = 1;
                Console.WriteLine("Куда хотите его поместить?");
                foreach (Cage cage in cages)
                {
                    Console.WriteLine("{0}){1}", i, cage.name);
                    i++;
                }
                int number = int.Parse(Console.ReadLine());
                cages[number - 1].Add(thisAnimal);
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
