using OOP_lab3.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
     public class Animal:Component
    {

        
        public int width { get; set; }

       
        //protected Animal successor;

        //public void SetSuccessor(Animal successor)
        //{
        //    this.successor = successor;
        //}

        public override void Signal()
        {
            throw new NotImplementedException();
        }

         

        public override int Display(int number, int depth)
        {
            throw new NotImplementedException();
        }

        public override int GetWidth()
        {
            throw new NotImplementedException();
        }

        public override int GetCount()
        {
            throw new NotImplementedException();
        }

        public override List<Component> Bust(List<Component> animals)
        {
            throw new NotImplementedException();
        }

        public Animal(string name, int width):base(name)
        {
           
            this.width = width;
        }
        // фабричный метод
        

    }
}
