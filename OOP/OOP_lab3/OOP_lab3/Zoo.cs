using OOP_lab3.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    public class Zoo
    {
        private All zoo;
        private Giraffe giraffe = new Giraffe();
        private Wolf wolf = new Wolf();
        private Bear bear = new Bear();
        bool isDay = true;
        List<Animal> sd = new List<Animal>();

        public Zoo()
        {
            zoo = new All();
            zoo.Add(giraffe);
            zoo.Add(wolf);
            zoo.Add(bear);
            Animal wolf1 = new Wolf("Волк", 100);
            wolf.Add(wolf1);
            sd.Add(wolf1);
            Animal bear1 = new Bear("Медведь", 300);
            bear.Add(bear1);
            sd.Add(bear1);
            Animal giraffe1 = new Giraffe("Жираф", 900);
            giraffe.Add(giraffe1);
            sd.Add(giraffe1);
        }

        public void Weiting()
        {
            Print();
            Console.WriteLine(
                "Выберите нужное действие:\n" +
                "1)Вывести общий вес животных в зверинце и средний вес одного животного\n" +
                "2)Добавить животное в зверинец\n" +
                "3)Сменить время суток\n" +
                "4)Подать голос");
            try
            {
                int i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        {
                            WeightAnimals();
                            break;
                        }
                    case 2:
                        {
                            AddAnimal();
                            break;
                        }
                    case 3:
                        {
                            ChangeDay();
                            break;
                        }
                    case 4:
                        {
                            ChoiceAnimal();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Некорректно введено число");
                            Console.ReadLine();
                            break;
                        }
                }
            }
            catch
            {
                Console.WriteLine("Некорректно введено число");
                Console.ReadLine();
                Weiting();
            }
            Weiting();
        }

        void ChangeDay()
        {
            isDay = isDay == true ? false : true;
            Console.WriteLine("Пора дня изменена успешно!");
            Console.ReadLine();
        }
        void AddAnimal()
        {
            Random r = new Random();
            int random = r.Next(1, 100);
            if (random < 21)
            {
                HaveGiraffe();
            }
            if (random < 61)
            {
                HaveVolf();
            }
            else
            {
                HaveBear();
            }
        }

        void HaveBear()
        {
            Console.WriteLine("Вам выпал медведь!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                bear.Add(new Bear(name, width));
                sd.Add(new Bear(name, width));
            Console.Write("Животное успешно добавлено в зоопарк!");
            Console.ReadLine();
                
            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveVolf()
        {
            Console.WriteLine("Вам выпал волк!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                wolf.Add(new Wolf(name, width));
                sd.Add(new Wolf(name, width));
            Console.Write("Животное успешно добавлено в зоопарк!");
            Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void HaveGiraffe()
        {
            Console.WriteLine("Вам выпал жираф!");
            Console.Write("Введите имя:");
            string name = Console.ReadLine();
            Console.Write("Введите вес:");
            try
            {
                int width = int.Parse(Console.ReadLine());
                giraffe.Add(new Giraffe(name, width));
                sd.Add(new Giraffe(name, width));
            Console.Write("Животное успешно добавлено в зоопарк!");
            Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Некорректно введенные данные!");
                Console.ReadLine();
            }
        }

        void WeightAnimals()
        {
            int sum = 0;
            foreach (Animal animal in sd)
            {
                sum += animal.Width;
            }
            Console.WriteLine("Общий вес животных:{0}, средний вес одного животного:{1}", sum, sum / sd.Count);
            Console.ReadKey();
        }

        void Print()
        {
            Console.WriteLine("\n\n\n------------------------------------------------------------");
            Console.WriteLine("Пора дня:{0}", isDay == true ? "День" : "Ночь");
           
            zoo.Display(1);
            Console.WriteLine("------------------------------------------------------------");
        }

        void ChoiceAnimal()
        {
            Console.WriteLine("\n\n\n\n\nКакие вы хотите услышать звуки?\n" +
                              "1)Всех вместе\n" +
                              "2)Определенного животного");
            try
            {
                int i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        {
                            AllAnimalsVote();
                            break;
                        }
                    case 2:
                        {
                            ChoiceOneAnimal();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Некорректно введено число");
                            Console.ReadLine();
                            break;
                        }
                }
            }
            catch
            {
                Console.WriteLine("Некорректно введено число");
                Console.ReadLine();
                ChoiceAnimal();
            }

        }
        void AllAnimalsVote()
        {
            if (isDay == false)
            {
                Console.WriteLine(
                    "К сожалению, нельзя будить всех животных. Попробуйте днем или выберите кого-то одного");
                Console.ReadLine();
                
            }
            for (int index = 0; index < sd.Count - 1; index++)
            {
                sd[index].SetSuccessor(sd[index + 1]);

            }
            sd[0].Signal();
            for (int index = 0; index < sd.Count - 1; index++)
            {
                sd[index].SetSuccessor(null);

            }
        }
        void ChoiceOneAnimal()
        {
            Console.WriteLine("Выберите из перечисленных животных кого хотите услышать");
            int z = 1;
            foreach (Animal animal in sd)
            {
                Console.WriteLine("{1}){0}", animal.Name, z);
                z++;
            }
            try
            {
                int r = int.Parse(Console.ReadLine());
                sd[r - 1].Signal();

            }
            catch
            {
                Console.WriteLine("Некорректно введено число");
                Console.ReadLine();
                ChoiceOneAnimal();
            }
        }
    }
}
