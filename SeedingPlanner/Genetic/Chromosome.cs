using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    class Chromosome : IChromosome
    {
        private double _fitness = 0;
        public double Fitness
        {
            get
            {
                return _fitness;
            }
        }

        private int _length;
        public int Length
        {
            get
            {
                return _length;
            }
        }

        private int[] _values;
        public int[] Values
        {
            get
            {
                return _values;
            }
        }

        private Random _random = new Random(DateTime.Now.Millisecond);

        public Chromosome(int length)
        {
            _length = length;
            _values = new int[_length];

            Generate();
        }

        public Chromosome(int[] values)
        {
            _length = values.Length;
            _values = values;
        }

        public Chromosome(Chromosome source)
        {
            _fitness = source._fitness;
            _length = source._length;
            _values = (int[]) source._values.Clone();
        }

        public void Generate()
        {
            for (int i = 0; i < _length; ++i)
            {
                _values[i] = i;
            }

            for (int i = 0; i < _length; ++i)
            {
                int tmp;
                int index1 = _random.Next(_length);
                int index2 = _random.Next(_length);

                tmp = _values[index1];
                _values[index1] = _values[index2];
                _values[index2] = tmp;
            }
        }

        public void Mutate()
        {
            int tmp;
            int index1 = _random.Next(_length);
            int index2 = _random.Next(_length);

            tmp = _values[index1];
            _values[index1] = _values[index2];
            _values[index2] = tmp;
        }

        public void Crossover(IChromosome pair)
        {
            Chromosome p = (Chromosome)pair;

            int[] child1 = new int[_length];
            int[] child2 = new int[_length];

            if (p != null)
            {
                CreateChildFromTwoParents(_values, p._values, child1);
                CreateChildFromTwoParents(p._values, _values, child2);
            }
            else
            {
                SinglePointCrossoverFromTwoParents(_values, p._values, child1);
                SinglePointCrossoverFromTwoParents(p._values, _values, child2);
            }

            _values = child1;
            p._values = child2;
        }

        public void CreateChildFromTwoParents(int[] parent1, int[] parent2, int[] child)
        {
            bool[] isBusy = new bool[_length];
            int p1index = 0;
            int p2index = 0;
            
            for (int child_index = 0; child_index < _length; ++child_index)
            {
                // select a gene from parent1
                while (p1index < _length && isBusy[parent1[p1index]])
                {
                    ++p1index;
                }

                if (p1index < _length && !isBusy[parent1[p1index]])
                {
                    // valid selection
                    child[child_index] = parent1[p1index];
                    ++child_index;
                    isBusy[parent1[p1index]] = true;
                }

                // select a gene from parent2
                while (p2index < _length && isBusy[parent2[p2index]])
                {
                    ++p2index;
                }

                if (p2index < _length && !isBusy[parent2[p2index]])
                {
                    child[child_index] = parent2[p2index];
                    isBusy[parent2[p2index]] = true;
                }
            }
        }

        void SinglePointCrossoverFromTwoParents(int[] parent1, int[] parent2, int[] child)
        {
            int point_of_selection = _random.Next(_length);
            bool[] isBusy = new bool[_length];
            int child_index = 0;

            for (; child_index < point_of_selection; ++child_index)
            {
                // copy the sequence from parent1
                child[child_index] = parent1[child_index];
                isBusy[child[child_index]] = true;
            }

            for (int i = 0; i < _length; ++i)
            {
                if (!isBusy[parent2[i]])
                {
                    isBusy[parent2[i]] = true;
                    child[child_index] = parent2[i];
                    ++child_index;
                }
            }
        }

        public void Evaluate(IFitnessFunction fitnessFunction)
        {
            _fitness = fitnessFunction.Evaluate(this);
        }

        public int CompareTo(object o)
        {
            double f = ((Chromosome)o).Fitness;

            return (_fitness == f) ? 0 : (_fitness < f) ? 1 : -1;
        }

        public IChromosome CreateNew()
        {
            return new Chromosome(_length);
        }

        public IChromosome Clone()
        {
            return new Chromosome(this);
        }
    }
}
