using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace SeedingPlanner
{
    class SeedingPlan
    {
        private List<Tray> _trays;
        private List<Plate> _plates;
        private int[] _bagsOrder;

        public int TrayCount { get { return _trays.Count; } }
        public int PlateCount { get { return _plates.Count; } }

        public SeedingPlan()
        {
            _trays = new List<Tray>();
            _plates = new List<Plate>();
            _bagsOrder = null;
        }

        public void Setup(int[] bagsOrder)
        {
            // TODO: validate params

            _bagsOrder = bagsOrder;

            List<Seeding> seedings = new List<Seeding>();

            // populate trays and plates according to ordering
            Tray currTray = new Tray((_trays.Count+1).ToString("000"));

            for (int i = 0; i < BagsInventory.Count; ++i)
            {
                Bag bag = BagsInventory.GetAt(_bagsOrder[i]);

                if (currTray.isFull())
                {
                    _trays.Add(currTray);
                    currTray = new Tray((_trays.Count + 1).ToString("000"));
                }

                Seeding seeding = currTray.AddSeedsFromBag(bag);

                if (seeding != null)
                {
                    seedings.Add(seeding);
                }

                if (currTray.isFull())
                {
                    _trays.Add(currTray);
                    currTray = new Tray((_trays.Count+1).ToString("000"));
                    if (seeding != null && seeding.Count < bag.SeedsToPlant)
                    {
                        // tray is full, take another tray
                        seedings.Add(currTray.AddSeedsFromBag(bag, bag.SeedsToPlant - seeding.Count, bag.SeedsToSample - seeding.SamplesCount));
                    }
                }
            }
            // add the last tray
            if (currTray.SeedsCount > 0)
            {
                _trays.Add(currTray);
            }

            //Console.WriteLine("Added {0} Seedings to the setup", seedings.Count);


            Plate currPlate = new Plate((_plates.Count+1).ToString("000"));
            for (int i = 0; i < seedings.Count; i++)
            {
                Seeding seeding = seedings[i];

                while (seeding.SamplesCount > 0)
                {
                    SampleGroup sg = currPlate.AddSamples(seeding, seeding.SamplesCount);

                    if (currPlate.isFull())
                    {
                        // plate is full, take another plate
                        _plates.Add(currPlate);
                        currPlate = new Plate((_plates.Count + 1).ToString("000"));
                    }
                    seeding.SamplesCount -= sg.Count;
                }
            }
            // add the last plate
            if (currPlate.SeedsCount > 0)
            {
                _plates.Add(currPlate);
            }

            //Console.WriteLine("Added {0} trays to the setup", _trays.Count);
            //Console.WriteLine("Added {0} plates to the setup", _plates.Count);
            //Console.WriteLine("Plan cost is: {0}", Cost());
        }

        public void SaveToExcel(string filename)
        {
            ExcelWriter excel = new ExcelWriter();

            // write bags
            for (int i = 0; i < BagsInventory.Count; ++i)
            {
                Bag bag = BagsInventory.GetAt(_bagsOrder[i]);
                excel.AddBag(bag);
            }

            // write trays
            for (int i = 0; i < _trays.Count; i++)
            {
                Tray tray = _trays[i];
                excel.AddTray(tray);
            }

            // write plates
            for (int i = 0; i < _plates.Count; i++)
            {
                Plate plate = _plates[i];
                excel.AddPlate(plate);
            }

            excel.SaveToFile(filename);
        }

        public double Cost()
        {
            double cost = 0.0;

            foreach (Plate p in _plates)
            {
                cost += (p.NumberOfSamples * p.SeedsCount);
            }

            return cost * -1.0;
        }

        public int Cost(int trayCost, int sampleCost)
        {
            int cost = 0;

            cost += (_trays.Count * trayCost);
            foreach (Plate p in _plates)
            {
                cost += (p.NumberOfSamples * p.SeedsCount);
                if (p.TraysCount > 2)
                {
                    // add an artifical factor to avoid too many trays in a plate
                    cost = (int)(cost * (1 + (p.TraysCount * 0.1)));
                }
            }

            // cost is a negative value
            return cost * -1;
        }
        
    }
}
