using OOP_lab3.Abstract;
using OOP_lab3.Animals;
using OOP_lab3.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    public class Zoo
    {
        
        bool isDay = true;
       

        public static Cage giraffes = new Cage("Вольер жирафов");
        public static Cage wolfs = new Cage("Вольер волков");
        public static Cage bears = new Cage("Вольер медведей");
            Cage zoo = new Cage("Зоопарк");
        public Zoo()
        {
            zoo.Add(giraffes);
            zoo.Add(wolfs);
            zoo.Add(bears);
            Animal wolf1 = new Wolf("Волк", 100);
            wolfs.Add(wolf1);
            Animal bear1 = new Bear("Медведь", 300);
            bears.Add(bear1);
            Animal giraffe1 = new Giraffe("Жираф", 900);
            giraffes.Add(giraffe1);
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
            Create r = new Create(zoo);
        }

        

        void WeightAnimals()
        {
            int width = zoo.GetWidth();
            int count = zoo.GetCount();
           
            Console.WriteLine("Общий вес животных:{0}, средний вес одного животного:{1}", width, width / count);
            Console.ReadKey();
        }

        void Print()
        {
            Console.WriteLine("\n\n\n------------------------------------------------------------");
            Console.WriteLine("Пора дня:{0}", isDay == true ? "День" : "Ночь");
           
            zoo.Display(1, 0);
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
            zoo.Signal();          
        }
        void ChoiceOneAnimal()
        {
            Console.WriteLine("Выберите из перечисленных животных кого хотите услышать");
            int z = 1;
            zoo.Display(1, 0);
            try
            {
                int r = int.Parse(Console.ReadLine());
                List<Component> list = new List<Component>();
                list=zoo.Bust(list);
                list[r - 1].Signal();

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
