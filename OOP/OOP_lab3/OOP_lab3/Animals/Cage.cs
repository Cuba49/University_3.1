using OOP_lab3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3.Animals
{
    public class Cage : Component
    {
        public string name ;

        public Cage(string name):base(name)
        {
            this.name = name;
        }
        List<Component> childrens = new List<Component>(); 
        public override int Display(int number, int depth)
        {
            Console.WriteLine(new String('-', depth * 3) + "{0}:", name);
            if (childrens != null)
            {
                depth++;
                foreach (Component children in childrens)
                {
                    number = children.Display(number, depth);
                }
            }
            return number;
        }

        public override void Signal()
        {
            if (childrens != null)
            {
                foreach (Component children in childrens)
                {
                    children.Signal();
                }
            }
        }

        public void Add(Component component)
        {
            childrens.Add(component);
        }

        public override int GetWidth()
        {
            int sum = 0;
            if (childrens != null)
            {
                foreach (Component children in childrens)
                {
                    sum+=children.GetWidth();
                }
            }
            return sum;
        }

        public override int GetCount()
        {
            int sum = 0;
            if (childrens != null)
            {
                foreach (Component children in childrens)
                {
                    sum += children.GetCount();
                }
            }
            return sum;
        }

        public override List<Component> Bust(List<Component> animal)
        {
            if (childrens != null)
            {
                foreach (Component children in childrens)
                {
                    animal = children.Bust(animal);
                }
            }
            return animal;
        }

        public override List<Cage> NumberCage(Animal animal, List<Cage> cages)
        {
            if (childrens == null)
            {
                if (!cages.Contains(this))
                {
                    cages.Add(this);
                }
            }
            if (childrens != null)
            {

                bool isCan = true;
                foreach (Component children in childrens)
                {
                    if (children.GetType() == this.GetType())
                    {
                        if (cages.Count != (children.NumberCage(animal, cages)).Count) ;
                        cages = children.NumberCage(animal, cages);
                       }
                    
                    else
                    if (!(children.GetType() == animal.GetType()))
                    {
                        isCan = false;
                    }
                    }
                if (isCan&&!cages.Contains(this))
                {
                    cages.Add(this);

                }

            }
            return cages;
        }
    }
}
