using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner
{
    class Bag
    {
        public string BagName { set; get; }
        public string FieldName { set; get; }
        public string Comment { set; get; }
        public int SeedsToPlant { set; get; }
        public int SeedsToSample { set; get; }
        public SortedSet<string> Samples { set; get; }

        public Bag(string name, string field, int toPlant, int toSample, string samples, string comment)
        {
            BagName = name;
            FieldName = field;
            Comment = comment;
            SeedsToPlant = toPlant;
            SeedsToSample = toSample;
            Samples = new SortedSet<string>();
            samples = samples.Replace(" ", ",");
            foreach (string s in samples.Split(','))
            {
                if (!string.IsNullOrEmpty(s.Trim()))
                {
                    Samples.Add(s.Trim());
                }
            }

            // clear the number of seeds to sample in case there are no samples in the list
            if (Samples.Count == 0)
            {
                SeedsToSample = 0;
            }
        }

    }
}
