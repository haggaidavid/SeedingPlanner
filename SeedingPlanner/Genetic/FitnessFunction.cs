using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    class FitnessFunction : IFitnessFunction
    {
        public double Evaluate(IChromosome chromosome)
        {
            Chromosome c = (Chromosome)chromosome;

            SeedingPlan seedingPlan = new SeedingPlan();

            seedingPlan.Setup(c.Values);
            double cost = seedingPlan.Cost();

            return cost;
        }
    }
}
