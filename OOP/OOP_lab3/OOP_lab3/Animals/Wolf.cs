using OOP_lab3.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    public class Wolf : Animal
    {
        public int Number;
        public Wolf(string name, int width) : base(name, width)
        { }

        public override void Signal()
        {
            Console.WriteLine("Волк {0} издает звуки", name);
        }

        public override int Display(int number, int depth)
        {
            Console.WriteLine("{2})"+new String('-', depth*3) + "Имя:{0}, Вес:{1}", name, width, number);
            Number = number;
            number++;
            return number;
        }

        public override int GetWidth()
        {
            return width;
        }
        public override int GetCount()
        {
            return 1;
        }
        public override List<Component> Bust(List<Component> animal)
        {
            animal.Add(this);
            return animal;
        }

    }
}
