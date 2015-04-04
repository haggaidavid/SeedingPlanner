using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner
{
    class Bag
    {
        public static readonly Bag EmptyBag = new Bag(); 
        
        
        public string BagName { set; get; }
        public string FieldName { set; get; }
        public string Comment { set; get; }
        public int SeedsToPlant { set; get; }
        public int SeedsToSample { set; get; }
        public SortedSet<string> Samples { set; get; }
        public List<Tuple<Tray, int, int>> Trays { set; get; }
        public List<Tuple<Plate, int>> Plates { set; get; }

        public Bag()
        {
            BagName = "";
            FieldName = "";
            Comment = "";
            SeedsToPlant = 0;
            SeedsToSample = 0;
            Samples = null;
            Trays = new List<Tuple<Tray, int, int>>();
            Plates = new List<Tuple<Plate, int>>();
        }

        public Bag(string name, string field, int toPlant, int toSample, string samples, string comment)
        {
            BagName = name;
            FieldName = field;
            Comment = comment;
            SeedsToPlant = toPlant;
            SeedsToSample = toSample;
            Samples = new SortedSet<string>();
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

            Trays = new List<Tuple<Tray, int, int>>();
            Plates = new List<Tuple<Plate, int>>();
        }
    }
}
