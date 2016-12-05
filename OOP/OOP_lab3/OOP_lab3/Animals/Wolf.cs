using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    class Wolf : Animal
    {
        public Wolf()
        {
            
        }
        public Wolf(string name, int width) : base(name, width)
        { }

        public override void Signal()
        {
            if (successor != null)
            {
                successor.Signal();
            }
            Vote X = new WolfSignal();
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
                Console.WriteLine(new String('-', depth) + "Волки:");
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
