using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    public interface IChromosome : IComparable
    {
        double Fitness { get; }

        void Generate();

        IChromosome CreateNew();

        IChromosome Clone();

        void Mutate();

        void Crossover(IChromosome pair);

        void Evaluate(IFitnessFunction function);
    }
}
