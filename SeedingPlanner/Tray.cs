using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SeedingPlanner
{
    class Tray
    {
        public string Name { get; set; }

        private int _seedsCount;
        public int SeedsCount { get { return _seedsCount; } }

        private List<Seeding> _seeding;
        public List<Seeding> Seedings { get { return _seeding; } }

        public int BagsCount
        {
            get
            {
                SortedSet<string> bags = new SortedSet<string>();
                foreach (Seeding s in _seeding)
                {
                    bags.Add(s.Bag.BagName);
                }
                return bags.Count;
            }
        }

        private int _nextAvailableRow;

        public Tray() : this("") {}

        public Tray(string name)
        {
            Name = name;
            _seedsCount = 0;

            _seeding = new List<Seeding>();
            _nextAvailableRow = 0;
        }

        public bool isFull()
        {
            return _nextAvailableRow >= Config.Application.Tray.NumberOfRows;
        }

        public Seeding AddSeedsFromBag(Bag bag)
        {
            return AddSeedsFromBag(bag, bag.SeedsToPlant, bag.SeedsToSample);
        }

        public Seeding AddSeedsFromBag(Bag bag, int seedsToAdd, int seedsToSample)
        {
            int seedsAdded = 0;
            int samplesAdded = 0;
            Seeding seeding = null;

            if (!isFull())
            {
                int rowsAdded = 0;
                int fromRow = _nextAvailableRow;
                int toRow = fromRow;

                int rowsRequired = (int)Math.Ceiling((double)(seedsToAdd / Config.Application.Tray.NumberOfCellsInRow));
                if (rowsRequired > (Config.Application.Tray.NumberOfRows - _nextAvailableRow))
                {
                    toRow = Config.Application.Tray.NumberOfRows - 1;
                }
                else
                {
                    toRow = fromRow + rowsRequired - 1;
                }
                rowsAdded = (toRow - fromRow) + 1;
                _nextAvailableRow += rowsAdded;

                if (!isFull())
                {
                    // add an empty row
                    _nextAvailableRow++;
                }

                seedsAdded = rowsAdded * Config.Application.Tray.NumberOfCellsInRow;
                _seedsCount += seedsAdded;

                if (seedsAdded >= seedsToSample)
                {
                    samplesAdded = seedsToSample;
                }
                else
                {
                    samplesAdded = seedsAdded;
                }
                seeding = new Seeding(bag, this, fromRow, toRow, seedsAdded, samplesAdded);
                _seeding.Add(seeding);
            }

            return seeding;
        }
    }
}
