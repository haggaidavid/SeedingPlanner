using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SeedingPlanner
{
    class Plate
    {
        public string Name { get; set; }

        private int _seedsCount;
        public int SeedsCount { get { return _seedsCount; } }

        // samples to take from this tray
        private SortedSet<string> _samples;
        public int NumberOfSamples { get { return _samples.Count; } }
        public string Samples
        {
            get
            {
                return string.Join(",", _samples);
            }
        }

        private List<SampleGroup> _sampleGroups;
        public List<SampleGroup> SampleGroups { get { return _sampleGroups; } }

        public int TraysCount
        {
            get
            {
                SortedSet<string> trays = new SortedSet<string>();
                foreach (SampleGroup sg in _sampleGroups)
                {
                    trays.Add(sg.Seeding.Tray.Name);
                }
                return trays.Count;
            }
        }


        public Plate(string name)
        {
            Name = name;
            _seedsCount = 0;
            _samples = new SortedSet<string>();
            _sampleGroups = new List<SampleGroup>();
        }

        public bool isFull()
        {
            return _seedsCount >= Config.Plate.NumberOfSamples;
        }

        public SampleGroup AddSamples(Seeding seeding, int seedsToAdd)
        {
            int seedsAdded = 0;
            SampleGroup sg = null;

            if (!isFull())
            {
                if (seedsToAdd > 0 && seeding.Bag.SeedsToSample > 0)
                {
                    if (seedsToAdd <= (Config.Plate.NumberOfSamples - _seedsCount))
                    {
                        seedsAdded = seedsToAdd;
                    }
                    else
                    {
                        seedsAdded = Config.Plate.NumberOfSamples - _seedsCount;
                    }

                    _seedsCount += seedsAdded;
                    _samples.UnionWith(seeding.Bag.Samples);

                    sg = new SampleGroup(seeding, seedsAdded);
                    _sampleGroups.Add(sg);
                }
            }

            return sg;
        }

    }
}
