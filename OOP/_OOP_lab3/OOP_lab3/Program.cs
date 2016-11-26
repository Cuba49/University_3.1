using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab3
{
    class MainApp
    {
        static void Main()
        {
            List<AnimalType> animalTypes = new List<AnimalType>();
            animalTypes.Add(new AnimalType("Жираф"));
            animalTypes.Add(new AnimalType("Волк"));
            animalTypes.Add(new AnimalType("Медведь"));


            //// Create a tree structur
            //Composite root = new Composite("root");

            //root.Add(new Leaf("Leaf A"));
            //root.Add(new Leaf("Leaf B"));

            //Composite comp = new Composite("Composite X");

            //comp.Add(new Leaf("Leaf XA"));
            //comp.Add(new Leaf("Leaf XB"));
            //root.Add(comp);
            //root.Add(new Leaf("Leaf C"));

            //// Add and remove a leaf
            //Leaf leaf = new Leaf("Leaf D");
            //root.Add(leaf);
            //root.Remove(leaf);

            //// Recursively display tree
            //root.Display(1);
       

            //// Wait for user
            //Console.Read();
        }
    }

   
    class AnimalType
    {
        string name;
        public AnimalType(string name) 
        {
            this.name = name;
        }
        
    }
    
    abstract class Animal
    {
        string name;
        int weight;

        public abstract void Display(int depth);
        
    }

   //class Zoo
   // {
   //     public Zoo{
   //     animalTypes.Add(new AnimalType("Жираф"));
   //         animalTypes.Add(new AnimalType("Волк"));
   //         animalTypes.Add(new AnimalType("Медведь"));

   //         }


   //     private ArrayList children = new ArrayList();
   //     public void Add(Animal component)
   //     {
   //         children.Add(component);
   //     }

   //     public void Remove(Animal component)
   //     {
   //         children.Remove(component);
   //     }
        
   // }
    class Bear : Animal
    {
      
        // Constructor
        public Bear(string name) : base(name)
        {
        }

       

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }

    /// <summary>
    /// Leaf - лист
    /// </summary>
    /// <remarks>
    /// <li>
    /// <lu>представляет листовой узел композиции и не имеет потомков;</lu>
    /// <lu>определяет поведение примитивных объектов в композиции;</lu>
    /// </li>
    /// </remarks>
    class Leaf : Component
    {
        // Constructor
        public Leaf(string name) : base(name)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }
    }
}
