using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    class SelectionMethod
    {
        public SelectionMethod() { }

        public void ApplySelectio(List<IChromosome> chromosomes, int size)
        {
            chromosomes.Sort();
            chromosomes.RemoveRange(size, chromosomes.Count - size);
        }
    }
}
