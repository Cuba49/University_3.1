using System.Collections.Generic;
using System.Threading;

namespace lab2.Interfaces
{
    public interface ICombinationsContainer
    {
        Mutex Mutex { get; }

        ICollection<INumbersCombination> Combinations { get; }
        INumbersCombination TryGetCombination();
    }
}