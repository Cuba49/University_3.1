using lab2.Interfaces;

namespace lab2.Models
{
    public struct NumbersCombination : INumbersCombination
    {
        public NumbersCombination(int a, int b, int c, int d)
            : this()
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public int A { get; }
        public int B { get; }
        public int C { get; }
        public int D { get; }
    }
}