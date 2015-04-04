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
        // configuration settings
        public static int SAMPLES_COUNT = Convert.ToInt32(ConfigurationManager.AppSettings["Plate_SamplesCount"]);

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

        private List<Bag> _bags;
        public List<Bag> Bags { get { return _bags; } }

        public Plate(string name)
        {
            Name = name;
            _seedsCount = 0;
            _samples = new SortedSet<string>();
            _bags = new List<Bag>();
        }

        public bool isFull()
        {
            return _seedsCount >= SAMPLES_COUNT;
        }
        
        public int AddSamples(Bag bag)
        {
            return AddSamples(bag, bag.SeedsToSample);
        }

        public int AddSamples(Bag bag, int seedsToAdd)
        {
            int seedsAdded = 0;

            if (seedsToAdd > 0 && bag.SeedsToSample > 0)
            {
                if (seedsToAdd <= (SAMPLES_COUNT - _seedsCount))
                {
                    seedsAdded = seedsToAdd;
                }
                else
                {
                    seedsAdded = SAMPLES_COUNT - _seedsCount;
                }
                _seedsCount += seedsAdded;
                _samples.UnionWith(bag.Samples);
                _bags.Add(bag);
                bag.Plates.Add(new Tuple<Plate, int>(this, seedsAdded));
            }

            return seedsAdded;
        }

        public string AsString()
        {
            string str = "";

            str += Name;
            str += ": ";
            foreach (Bag b in _bags)
            {
                str += b.BagName;
                str += ", ";
            }
            str += "\n";

            return str;
        }
    }
}
