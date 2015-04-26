using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner
{
    class SampleGroup
    {
        public Seeding Seeding { get; set; }
        public int Count { get; set; }

        public SampleGroup() { }

        public SampleGroup(Seeding seeding, int count)
        {
            Seeding = seeding;
            Count = count;
        }
    }
}
