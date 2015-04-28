using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    public interface IFitnessFunction
    {
        double Evaluate(IChromosome chromosome);
    }
}
