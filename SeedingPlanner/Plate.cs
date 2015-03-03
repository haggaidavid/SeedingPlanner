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

        private HashSet<Tray> _trays;


        public Plate(string name)
        {
            Name = name;
            _seedsCount = 0;
            _samples = new SortedSet<string>();
            _trays = new HashSet<Tray>();
        }

        public bool isFull()
        {
            return _seedsCount >= SAMPLES_COUNT;
        }
        
        public int AddSamples(Tray tray, Bag bag)
        {
            return AddSamples(tray, bag, bag.SeedsToSample);
        }

        public int AddSamples(Tray tray, Bag bag, int seedsToAdd)
        {
            int seedsAdded = 0;

            if (seedsToAdd > 0)
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
                _trays.Add(tray);
            }

            return seedsAdded;
        }

        public string AsString()
        {
            string str = "";

            str += Name;
            str += ": ";
            foreach (Tray t in _trays)
            {
                str += t.Name;
                str += ", ";
            }
            str += "\n";

            return str;
        }
    }
}
