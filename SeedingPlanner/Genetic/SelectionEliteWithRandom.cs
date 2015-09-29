using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner.Genetic
{
    class SelectionEliteWithRandom : ISelectionMethod
    {
        private double m_elitePercentage = 0.25; // 25% of elite are preserved.

        public SelectionEliteWithRandom() { }

        public SelectionEliteWithRandom(double elitePercentage) 
        {
            m_elitePercentage = elitePercentage; 
        }

        public void ApplySelection(List<IChromosome> chromosomes, int size)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int minimum_index = (int)Math.Ceiling(size * m_elitePercentage);

            chromosomes.Sort();
            
            while (chromosomes.Count > size)
            {
                int index = random.Next(chromosomes.Count - minimum_index) + minimum_index;
                chromosomes.RemoveAt(index);
            }
            //chromosomes.RemoveRange(size, chromosomes.Count - size);
        }
    }
}

