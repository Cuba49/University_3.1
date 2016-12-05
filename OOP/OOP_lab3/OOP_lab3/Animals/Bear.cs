using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    class Bear : Animal
    {
        public Bear()
        {
            
        }
        public Bear(string name, int width) : base(name, width)
        { }

        public override void Signal()
        {
            if (successor != null)
            {
                successor.Signal();
            }
            Vote X = new BearSignal();
            Console.WriteLine(X.GetVote());
        }

        private ArrayList children = new ArrayList();

        public void Add(Animal animal)
        {
            children.Add(animal);
        }

        public override void Display(int depth)
        {
            if (depth == 1)
            {
                Console.WriteLine(new String('-', depth) + "Медведи:");
            }
            else
            {
                Console.WriteLine(new String('-', depth) + "Имя: {0}, Вес: {1}", Name, Width);
            }

            foreach (Animal animal in children)
            {
                animal.Display(depth + 2);
            }
        }
    }


}

