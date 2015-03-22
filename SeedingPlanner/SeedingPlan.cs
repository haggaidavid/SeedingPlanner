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
        //BagsInventory _bags;
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

            // add the last tray
            if (currTray.SeedsCount > 0)
            {
                _trays.Add(currTray);
            }

            // add the last plate
            if (currPlate.SeedsCount > 0)
            {
                _plates.Add(currPlate);
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

        public void WritePlates()
        {
            Console.WriteLine("");
            Console.WriteLine("{0} Plates", PlateCount);
            Console.WriteLine("");
            foreach (Plate p in _plates)
            {
                Console.WriteLine(p.AsString());
            }
        }

        public bool SaveToExcel(string filename)
        {
            IRow row = null;

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet bagsSheet = workbook.CreateSheet("Seed Bags");
            bagsSheet.IsRightToLeft = true;
            bagsSheet.CreateFreezePane(0, 1);

            int rowIndex = 0;
            IRow headerRow = bagsSheet.CreateRow(rowIndex);
            headerRow.CreateCell(Config.ColumnNumber.FIELD_NAME).SetCellValue(Config.ColumnName.FIELD_NAME);
            headerRow.CreateCell(Config.ColumnNumber.BAG_NAME).SetCellValue(Config.ColumnName.BAG_NAME);
            headerRow.CreateCell(Config.ColumnNumber.SEEDS_TO_PLANT).SetCellValue(Config.ColumnName.SEEDS_TO_PLANT);
            headerRow.CreateCell(Config.ColumnNumber.SEEDS_TO_SAMPLE).SetCellValue(Config.ColumnName.SEEDS_TO_SAMPLE);
            headerRow.CreateCell(Config.ColumnNumber.SAMPLES).SetCellValue(Config.ColumnName.SAMPLES);
            headerRow.CreateCell(Config.ColumnNumber.COMMENT).SetCellValue(Config.ColumnName.COMMENT);


            for (int i = 0; i < _bagsOrder.Length; ++i)
            {
                rowIndex++;
                Bag bag = BagsInventory.GetAt(_bagsOrder[i]);
                row = bagsSheet.CreateRow(rowIndex);
                row.CreateCell(Config.ColumnNumber.FIELD_NAME).SetCellValue(bag.FieldName);
                row.CreateCell(Config.ColumnNumber.BAG_NAME).SetCellValue(bag.BagName);
                row.CreateCell(Config.ColumnNumber.SEEDS_TO_PLANT).SetCellValue(bag.SeedsToPlant);
                if (bag.SeedsToSample > 0)
                {
                    row.CreateCell(Config.ColumnNumber.SEEDS_TO_SAMPLE).SetCellValue(bag.SeedsToSample);
                }
                row.CreateCell(Config.ColumnNumber.SAMPLES).SetCellValue(string.Join(",", bag.Samples.ToArray()));
                row.CreateCell(Config.ColumnNumber.COMMENT).SetCellValue(bag.Comment);
            }
            bagsSheet.AutoSizeColumn(Config.ColumnNumber.BAG_NAME);
            bagsSheet.AutoSizeColumn(Config.ColumnNumber.SAMPLES);
            bagsSheet.AutoSizeColumn(Config.ColumnNumber.COMMENT);

            ISheet traysSheet = workbook.CreateSheet("Trays");
            traysSheet.IsRightToLeft = true;
            traysSheet.CreateFreezePane(0, 1);
            rowIndex = 0;

            // create header row
            row = traysSheet.CreateRow(rowIndex);
            row.CreateCell(Config.ColumnNumber.BAG_NAME_IN_TRAY).SetCellValue(Config.ColumnName.BAG_NAME);
            row.CreateCell(Config.ColumnNumber.FROM_ROW).SetCellValue(Config.ColumnName.FROM_ROW_NAME);
            row.CreateCell(Config.ColumnNumber.TO_ROW).SetCellValue(Config.ColumnName.TO_ROW_NAME);
            row.CreateCell(Config.ColumnNumber.TO_ROW + 1).SetCellValue("amidut");
            row.CreateCell(Config.ColumnNumber.TO_ROW + 2).SetCellValue("PCR");

            rowIndex++;

            for (int i = 0; i < _trays.Count; ++i)
            {
                // one space row between trays
                rowIndex++;

                Tray tray = _trays[i];
                List<Tuple<Bag, int, int>> bags = tray.Bags;
                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.WrapText = true;

                // create string for the description row
                string trayDescription = Config.SheetFormat.TRAY_DESCRIPTION_FORMAT;
                trayDescription = trayDescription.Replace("__TRAY_NUMBER__", (i + 1).ToString());
                trayDescription = trayDescription.Replace("__SEEDS_COUNT__", tray.SeedsCount.ToString());
                trayDescription = trayDescription.Replace("__BAGS_COUNT__", bags.Count.ToString());

                row = traysSheet.CreateRow(rowIndex);
                row.CreateCell(Config.ColumnNumber.TRAY_DESCRIPTION).SetCellValue(trayDescription);
                CellRangeAddress trayHeader = new CellRangeAddress(rowIndex, rowIndex, Config.ColumnNumber.BAG_NAME_IN_TRAY, Config.ColumnNumber.TO_ROW);
                traysSheet.AddMergedRegion(trayHeader);

                rowIndex++;

                for (int b = 0; b < bags.Count; ++b)
                {
                    Bag bag = bags[b].Item1;
                    int fromRow = bags[b].Item2;
                    int toRow = bags[b].Item3;

                    row = traysSheet.CreateRow(rowIndex);
                    row.CreateCell(1).SetCellValue(bag.BagName);
                    row.CreateCell(2).SetCellValue(fromRow+1);
                    row.CreateCell(3).SetCellValue(toRow+1);
                    row.CreateCell(4).SetCellValue(bag.SeedsToSample);
                    row.CreateCell(5).SetCellValue(String.Join(",", bag.Samples));

                    rowIndex++;
                }

            }
            traysSheet.AutoSizeColumn(Config.ColumnNumber.BAG_NAME_IN_TRAY);


            try
            {
                FileStream fileStream = new FileStream(filename, FileMode.Create);
                workbook.Write(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }

            return true;
        }
    }
}
