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
        // configuration settings
        public static int ROWS_COUNT = Convert.ToInt32(ConfigurationManager.AppSettings["Tray_RowsCount"]);
        public static int CELLS_IN_ROW_COUNT = Convert.ToInt32(ConfigurationManager.AppSettings["Tray_CellsInRowCount"]);


        public string Name { get; set; }

        private int _seedsCount;
        public int SeedsCount { get { return _seedsCount; } }

        private int _seedsToSample;
        public int SeedsToSample { get { return _seedsToSample; } }

        // list of bags used to populate the tray
        // with row-start to row-end 
        private List<Tuple<Bag, int, int>> _bags;
        private int _nextAvailableRow;

        public Tray(string name)
        {
            Name = name;
            _seedsCount = 0;
            _seedsToSample = 0;

            _bags = new List<Tuple<Bag, int, int>>();
            _nextAvailableRow = 0;
        }

        public bool isFull()
        {
            return _nextAvailableRow >= ROWS_COUNT;
        }

        public int AddSeedsFromBag(Bag bag)
        {
            return AddSeedsFromBag(bag, bag.SeedsToPlant);
        }

        public int AddSeedsFromBag(Bag bag, int seedsToAdd)
        {
            int seedsAdded = 0;
            int rowsAdded = 0;
            int fromRow = _nextAvailableRow;
            int toRow = fromRow;

            if (isFull())
            {
                return 0;
            }

            int rowsRequired = (int)Math.Ceiling((double)(seedsToAdd / CELLS_IN_ROW_COUNT));
            if (rowsRequired > (ROWS_COUNT - _nextAvailableRow))
            {
                toRow = ROWS_COUNT - 1;
            }
            else
            {
                toRow = fromRow + rowsRequired - 1;
            }
            rowsAdded = (toRow - fromRow) + 1;
            _bags.Add(new Tuple<Bag, int, int>(bag, fromRow, toRow));
            _nextAvailableRow += rowsAdded;

            if (!isFull())
            {
                // add an empty row
                _nextAvailableRow++;
            }

            seedsAdded = rowsAdded * CELLS_IN_ROW_COUNT;
            _seedsCount += seedsAdded;

            // make sure the samples are taken from the right tray
            if (seedsAdded <= bag.SeedsToSample)
            {
                _seedsToSample += seedsAdded;
            }

            return seedsAdded;
        }

        public string AsString()
        {
            string str = "";

            str += Name;
            str += "\n";
            foreach (Tuple<Bag, int,int> tpl in _bags)
            {
                Bag bag = tpl.Item1;
                int fromRow = tpl.Item2;
                int toRow = tpl.Item3;

                str += bag.BagName;
                str += ",";
                str += " from: " + fromRow.ToString();
                str += " to: " + toRow.ToString();
                str += "\n";
            }

            return str;
        }
    }
}
