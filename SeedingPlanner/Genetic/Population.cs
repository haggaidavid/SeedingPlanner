using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    public class Population
    {
        private IFitnessFunction _fitnessFunction;
        private ISelectionMethod _selectionMethod;
        private List<IChromosome> _population = new List<IChromosome>();
        private int _size;

        private Random _random = new Random(DateTime.Now.Millisecond);
        private double _crossoverRate = 0.75;
        private double _mutateChance = 0.10;

        private double _maxFitness = 0.0;
        private double _avgFitness = 0.0;
        private double _sumFitness = 0.0;
        private IChromosome _bestChromosome = null;

        public double CrossoverRate
        {
            get
            {
                return _crossoverRate; 
            }
            set
            {
                _crossoverRate = Math.Max(0.1, Math.Min(1.0, value));
            }
        }

        public double MutationChance
        {
            get 
            {
                return _mutateChance;
            }
            set
            {
                _mutateChance = Math.Max(0.05, Math.Min(1.0, value));
            }
        }

        public ISelectionMethod SelectionMethod
        {
            get 
            {
                return _selectionMethod; 
            }
            set
            {
                _selectionMethod = value; 
            }
        }

        public IFitnessFunction FitnessFunction
        {
            get 
            {
                return _fitnessFunction;
            }
            set
            {
                _fitnessFunction = value;

                foreach (IChromosome member in _population)
                {
                    member.Evaluate(_fitnessFunction);
                }

                FindBestChromosome();
            }
        }

        public double MaxFitness
        {
            get 
            {
                return _maxFitness; 
            }
        }

        public double SumFitness
        {
            get
            {
                return _sumFitness;
            }
        }

        public double AvgFitness
        {
            get
            {
                return _avgFitness;
            }
        }

        public IChromosome BestChromosome
        {
            get
            {
                return _bestChromosome;
            }
        }

        public IChromosome this[int index]
        {
            get
            {
                return _population[index];
            }
        }

        public Population(int size, IChromosome ancestor, IFitnessFunction fitnessFunction, ISelectionMethod selectionMethod)
        {
            if (size < 2)
                throw new ArgumentException("Population too small");

            _size = size;
            _fitnessFunction = fitnessFunction;
            _selectionMethod = selectionMethod;

            // add the ancestor to the population
            ancestor.Evaluate(_fitnessFunction);
            _population.Add(ancestor);

            // generate more chromosomes and add them to the population
            for (int i = 0; i < _size; ++i)
            {
                IChromosome c = ancestor.CreateNew();
                c.Evaluate(_fitnessFunction);
                _population.Add(c);
            }
        }

        public void Crossover()
        {
            for (int i = 1; i < _size; i += 2)
            {
                if (_random.NextDouble() <= _crossoverRate)
                {
                    IChromosome c1 = _population[i - 1].Clone();
                    IChromosome c2 = _population[i].Clone();

                    c1.Crossover(c2);
                    c1.Evaluate(_fitnessFunction);
                    c2.Evaluate(_fitnessFunction);

                    _population.Add(c1);
                    _population.Add(c2);
                }
            }
        }

        public void Mutate()
        {
            for (int i = 0; i < _size; ++i)
            {
                if (_random.NextDouble() <= _mutateChance)
                {
                    IChromosome c = _population[i].Clone();
                    c.Mutate();
                    c.Evaluate(_fitnessFunction);
                    _population.Add(c);
                }
            }
        }

        public void Selection()
        {
            _selectionMethod.ApplySelection(_population, _size);
            FindBestChromosome();
        }

        public void RunEpoch()
        {
            Crossover();
            Mutate();
            Selection();
        }

        private void FindBestChromosome()
        {
            _bestChromosome = _population[0];
            _maxFitness = _bestChromosome.Fitness;
            _sumFitness = _maxFitness;

            for (int i = 1; i < _size; ++i)
            {
                double fitness = _population[i].Fitness;

                _sumFitness += fitness;

                if (fitness > _maxFitness)
                {
                    _maxFitness = fitness;
                }
            }
            _avgFitness = _sumFitness / _size;
        }

    }
}
