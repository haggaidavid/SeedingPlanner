using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner
{
    class SeedingPlan
    {
        //BagsInventory _bags;
        private List<Tray> _trays;
        private List<Plate> _plates;
        private int[] _bagsOrder;

        public SeedingPlan()
        {
            //_bags = bags;
            _trays = new List<Tray>();
            _plates = new List<Plate>();
            _bagsOrder = null;
        }

        public void Setup(int[] bagsOrder)
        {
            // TODO: validate params

            _bagsOrder = bagsOrder;

            // populate trays and plates according to ordering
            Tray currTray = new Tray("tray " + _trays.Count);
            Plate currPlate = new Plate("plate " + _plates.Count);
            for (int i = 0; i < BagsInventory.Count; ++i)
            {
                Bag bag = BagsInventory.GetAt(_bagsOrder[i]);
                int addedToTray = 0;
                int addedToPlate = 0;
                
                addedToTray += currTray.AddSeedsFromBag(bag);
                if (addedToTray < bag.SeedsToPlant)
                {
                    // tray is full, take another tray
                    _trays.Add(currTray);
                    addedToPlate = currPlate.AddSamples(currTray, bag, addedToTray);
                    if (addedToPlate < addedToTray)
                    {
                        _plates.Add(currPlate);
                        currPlate = new Plate("plate " + _plates.Count);
                        currPlate.AddSamples(currTray, bag, (addedToTray - addedToPlate));
                    }

                    currTray = new Tray("tray " + _trays.Count);
                    currTray.AddSeedsFromBag(bag, bag.SeedsToPlant - addedToTray);
                }

                addedToPlate = currPlate.AddSamples(currTray, bag, addedToTray);
                if (addedToPlate < addedToTray)
                {
                    _plates.Add(currPlate);
                    currPlate = new Plate("plate " + _plates.Count);
                    currPlate.AddSamples(currTray, bag, (addedToTray - addedToPlate));
                }
            }
        }

        public int Cost(int trayCost, int sampleCost)
        {
            int cost = 0;

            cost += (_trays.Count * trayCost);
            foreach (Plate p in _plates)
            {
                cost += (p.NumberOfSamples * p.SeedsCount);
            }

            return cost;
        }
    }
}
