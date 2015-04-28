using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    public interface ISelectionMethod
    {
        void ApplySelection(List<IChromosome> chromosomes, int size);
    }
}
