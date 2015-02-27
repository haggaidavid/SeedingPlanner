using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace SeedingPlanner
{
    class SeedingPlan
    {
        //BagsInventory _bags;
        private List<Tray> _trays;
        private List<Plate> _plates;
        private int[] _bagsOrder;

        public int TrayCount { get { return _trays.Count; } }
        public int PlateCount { get { return _plates.Count; } }

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

        public void WriteTrays()
        {
            Console.WriteLine("");
            Console.WriteLine("{0} Trays:", TrayCount);
            Console.WriteLine("");
            foreach (Tray t in _trays)
            {
                Console.WriteLine(t.AsString());
            }
        }

        public bool SaveToExcel(string filename)
        {
            IRow row = null;

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet bagsSheet = workbook.CreateSheet("Seed Bags");

            int rowIndex = 0;
            IRow headerRow = bagsSheet.CreateRow(rowIndex);
            headerRow.CreateCell(Config.ColumnNumber.FIELD_NAME).SetCellValue(Config.ColumnName.FIELD_NAME);
            headerRow.CreateCell(Config.ColumnNumber.BAG_NAME).SetCellValue(Config.ColumnName.BAG_NAME);
            headerRow.CreateCell(Config.ColumnNumber.SEEDS_TO_PLANT).SetCellValue(Config.ColumnName.SEEDS_TO_PLANT);
            headerRow.CreateCell(Config.ColumnNumber.SEEDS_TO_SAMPLE).SetCellValue(Config.ColumnName.SEEDS_TO_SAMPLE);
            headerRow.CreateCell(Config.ColumnNumber.SAMPLES).SetCellValue(Config.ColumnName.SAMPLES);
            headerRow.CreateCell(Config.ColumnNumber.COMMENT).SetCellValue(Config.ColumnName.COMMENT);


            rowIndex++;
            for (int i = 0; i < _bagsOrder.Length; ++i)
            {
                Bag bag = BagsInventory.GetAt(_bagsOrder[i]);
                row = bagsSheet.CreateRow(rowIndex);
                row.CreateCell(Config.ColumnNumber.FIELD_NAME).SetCellValue(bag.FieldName);
                row.CreateCell(Config.ColumnNumber.BAG_NAME).SetCellValue(bag.BagName);
                row.CreateCell(Config.ColumnNumber.SEEDS_TO_PLANT).SetCellValue(bag.SeedsToPlant);
                row.CreateCell(Config.ColumnNumber.SEEDS_TO_SAMPLE).SetCellValue(bag.SeedsToSample);
                row.CreateCell(Config.ColumnNumber.SAMPLES).SetCellValue(string.Join(",", bag.Samples.ToArray()));
                row.CreateCell(Config.ColumnNumber.COMMENT).SetCellValue(bag.Comment);
            }

            ISheet traysSheet = workbook.CreateSheet("Trays");
            rowIndex = 0;

            // TODO: create header row

            rowIndex++;

            for (int i = 0; i < _trays.Count; ++i)
            {
                // one space row between trays
                rowIndex++;

                Tray tray = _trays[i];
                List<Tuple<Bag, int, int>> bags = tray.Bags;

                // TODO: create string for the description row
                string trayDescription = "";
                
                for (int b = 0; b < bags.Count; ++b)
                {
                    Bag bag = bags[b].Item1;
                    int fromRow = bags[b].Item2;
                    int toRow = bags[b].Item3;

                    row = traysSheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(bag.BagName);
                    row.CreateCell(1).SetCellValue(fromRow);
                    row.CreateCell(2).SetCellValue(toRow);
                }

            }

            FileStream fileStream = new FileStream(filename, FileMode.Create);
            workbook.Write(fileStream);

            return true;
        }
    }
}
