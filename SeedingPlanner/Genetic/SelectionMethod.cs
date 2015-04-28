using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    class SelectionMethod : ISelectionMethod
    {
        public SelectionMethod() { }

        public void ApplySelection(List<IChromosome> chromosomes, int size)
        {
            chromosomes.Sort();
            chromosomes.RemoveRange(size, chromosomes.Count - size);
        }
    }
}
