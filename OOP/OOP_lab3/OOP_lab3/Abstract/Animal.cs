using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    abstract class Animal
    {
        public Animal()
        {
            
        }
        protected Animal successor;

        public void SetSuccessor(Animal successor)
        {
            this.successor = successor;
        }
        public string Name { get; set; }
        public int Width { get; set; }

        public Animal(string name, int width)
        {
            Name = name;
            Width = width;
        }
        // фабричный метод
        abstract public void Signal();
        public abstract void Display(int depth);

    }
}
