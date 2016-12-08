using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3.Abstract
{
     public abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }
        public abstract void Signal();
        public abstract int Display(int number,int depth);
        public abstract int GetWidth();
        public abstract int GetCount();
        public abstract List<Component> Bust(List<Component> animal);



    }
}
