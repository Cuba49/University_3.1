using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3.Animals
{
    class All : Animal
    {
        public override void Signal()
        {
            throw new NotImplementedException();
        }
        private ArrayList children = new ArrayList();

        public void Add(Animal animal)
        {
            children.Add(animal);
        }

        public override void Display(int depth)
        {
            Console.WriteLine("Состав зоопарка:");

            foreach (Animal animal in children)
            {
                animal.Display(depth);
            }
        }
    }
}
