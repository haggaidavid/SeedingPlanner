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
    class ExcelWriter
    {
        private XSSFWorkbook _workbook;

        private ISheet _bagsSheet;
        private int _bagsSheet_rowIndex;

        private ISheet _seedingSheet;
        private int _seedingSheet_rowIndex;

        private ISheet _samplingSheet;
        private int _samplingSheet_rowIndex;

        ICellStyle _headerStyle;

        public ExcelWriter()
        {
            _workbook = new XSSFWorkbook();
            InitializeSheets();
        }

        private void InitializeSheets()
        {
            _headerStyle = _workbook.CreateCellStyle();
            IFont headerFont = _workbook.CreateFont();
            headerFont.Boldweight = (short)FontBoldWeight.Bold;
            _headerStyle.SetFont(headerFont);

            InitializeBagsSheet();
            InitializeSeedingSheet();
            InitializeSamplingSheet();

        }

        private void InitializeBagsSheet()
        {
            _bagsSheet = _workbook.CreateSheet(Config.Excel.BagsSheet.SheetName);
            _bagsSheet.IsRightToLeft = true;
            _bagsSheet.CreateFreezePane(0, 1);
            _bagsSheet_rowIndex = 0;

            // header row
            IRow headerRow = _bagsSheet.CreateRow(_bagsSheet_rowIndex);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.FieldName.Index).SetCellValue(Config.Excel.BagsSheet.Columns.FieldName.Name);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.BagName.Index).SetCellValue(Config.Excel.BagsSheet.Columns.BagName.Name);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.SeedsToPlant.Index).SetCellValue(Config.Excel.BagsSheet.Columns.SeedsToPlant.Name);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.SeedsToSample.Index).SetCellValue(Config.Excel.BagsSheet.Columns.SeedsToSample.Name);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.Samples.Index).SetCellValue(Config.Excel.BagsSheet.Columns.Samples.Name);
            headerRow.CreateCell(Config.Excel.BagsSheet.Columns.Comment.Index).SetCellValue(Config.Excel.BagsSheet.Columns.Comment.Name);

            for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
            {
                headerRow.GetCell(i).CellStyle = _headerStyle;
            }
        }

        public void AddBag(Bag bag)
        {
            _bagsSheet_rowIndex++;

            IRow row = _bagsSheet.CreateRow(_bagsSheet_rowIndex);

            row.CreateCell(Config.Excel.BagsSheet.Columns.FieldName.Index).SetCellValue(bag.FieldName);
            row.CreateCell(Config.Excel.BagsSheet.Columns.BagName.Index).SetCellValue(bag.BagName);
            row.CreateCell(Config.Excel.BagsSheet.Columns.SeedsToPlant.Index).SetCellValue(bag.SeedsToPlant);
            if (bag.SeedsToSample > 0)
            {
                row.CreateCell(Config.Excel.BagsSheet.Columns.SeedsToSample.Index).SetCellValue(bag.SeedsToSample);
            }
            row.CreateCell(Config.Excel.BagsSheet.Columns.Samples.Index).SetCellValue(string.Join(",", bag.Samples.ToArray()));
            row.CreateCell(Config.Excel.BagsSheet.Columns.Comment.Index).SetCellValue(bag.Comment);
        }

        private void InitializeSeedingSheet()
        {
            _seedingSheet = _workbook.CreateSheet(Config.Excel.SeedingSheet.SheetName);
            _seedingSheet.IsRightToLeft = true;
            _seedingSheet.CreateFreezePane(0, 1);
            _seedingSheet_rowIndex = 0;

            IRow headerRow = _seedingSheet.CreateRow(_seedingSheet_rowIndex);
            headerRow.CreateCell(Config.Excel.SeedingSheet.Columns.BagName.Index).SetCellValue(Config.Excel.SeedingSheet.Columns.BagName.Name);
            headerRow.CreateCell(Config.Excel.SeedingSheet.Columns.FromRow.Index).SetCellValue(Config.Excel.SeedingSheet.Columns.FromRow.Name);
            headerRow.CreateCell(Config.Excel.SeedingSheet.Columns.ToRow.Index).SetCellValue(Config.Excel.SeedingSheet.Columns.ToRow.Name);
            headerRow.CreateCell(Config.Excel.SeedingSheet.Columns.SeedsToSample.Index).SetCellValue(Config.Excel.SeedingSheet.Columns.SeedsToSample.Name);
            headerRow.CreateCell(Config.Excel.SeedingSheet.Columns.PCR.Index).SetCellValue(Config.Excel.SeedingSheet.Columns.PCR.Name);

            for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
            {
                headerRow.GetCell(i).CellStyle = _headerStyle;
            }
        }

        public void AddTray(Tray tray)
        {
            _seedingSheet_rowIndex++; // one space row

            _seedingSheet_rowIndex++;
            IRow row = _seedingSheet.CreateRow(_seedingSheet_rowIndex);

            string description = Config.Excel.SeedingSheet.DescriptionFormat;
            description = description.Replace("__TRAY_NUMBER__", tray.Name);
            description = description.Replace("__SEEDS_COUNT__", tray.SeedsCount.ToString());
            description = description.Replace("__BAGS_COUNT__", tray.BagsCount.ToString());
            row.CreateCell(Config.Excel.SeedingSheet.Columns.BagName.Index).SetCellValue(description);

            CellRangeAddress trayDescriptionRange = new CellRangeAddress(_seedingSheet_rowIndex, _seedingSheet_rowIndex,
                Config.Excel.SeedingSheet.Columns.BagName.Index,
                Config.Excel.SeedingSheet.Columns.PCR.Index);
            _seedingSheet.AddMergedRegion(trayDescriptionRange);

            foreach (Seeding seeding in tray.Seedings)
            {
                AddSeeding(seeding);
            }
        }

        public void AddSeeding(Seeding seeding)
        {
            _seedingSheet_rowIndex++;
            IRow row = _seedingSheet.CreateRow(_seedingSheet_rowIndex);

            row.CreateCell(Config.Excel.SeedingSheet.Columns.BagName.Index).SetCellValue(seeding.Bag.BagName);
            row.CreateCell(Config.Excel.SeedingSheet.Columns.FromRow.Index).SetCellValue(seeding.FromRow);
            row.CreateCell(Config.Excel.SeedingSheet.Columns.ToRow.Index).SetCellValue(seeding.ToRow);
            row.CreateCell(Config.Excel.SeedingSheet.Columns.SeedsToSample.Index).SetCellValue(seeding.Count);
            row.CreateCell(Config.Excel.SeedingSheet.Columns.PCR.Index).SetCellValue(string.Join(",", seeding.Bag.Samples));
        }

        private void InitializeSamplingSheet()
        {
            _samplingSheet = _workbook.CreateSheet(Config.Excel.SamplingSheet.SheetName);
            _samplingSheet.IsRightToLeft = true;
            _samplingSheet.CreateFreezePane(0, 1);
            _samplingSheet_rowIndex = 0;

            IRow headerRow = _samplingSheet.CreateRow(_samplingSheet_rowIndex);
            headerRow.CreateCell(Config.Excel.SamplingSheet.Columns.Tray.Index).SetCellValue(Config.Excel.SamplingSheet.Columns.Tray.Name);
            headerRow.CreateCell(Config.Excel.SamplingSheet.Columns.Rows.Index).SetCellValue(Config.Excel.SamplingSheet.Columns.Rows.Name);
            headerRow.CreateCell(Config.Excel.SamplingSheet.Columns.BagName.Index).SetCellValue(Config.Excel.SamplingSheet.Columns.BagName.Name);
            headerRow.CreateCell(Config.Excel.SamplingSheet.Columns.Count.Index).SetCellValue(Config.Excel.SamplingSheet.Columns.Count.Name);
            headerRow.CreateCell(Config.Excel.SamplingSheet.Columns.PCR.Index).SetCellValue(Config.Excel.SamplingSheet.Columns.PCR.Name);

            for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
            {
                headerRow.GetCell(i).CellStyle = _headerStyle;
            }
        }

        public void AddPlate(Plate plate)
        {
            _samplingSheet_rowIndex++; // space row

            _samplingSheet_rowIndex++;
            IRow row = _samplingSheet.CreateRow(_samplingSheet_rowIndex);

            int traysCount = plate.TraysCount;
            string description = Config.Excel.SamplingSheet.DecriptionFormat;
            description = description.Replace("__PLATE_NAME__", plate.Name);
            description = description.Replace("__SEEDS_COUNT__", plate.SeedsCount.ToString());
            description = description.Replace("__SAMPLES__", plate.Samples);
            description = description.Replace("__TRAYS_COUNT__", traysCount.ToString());
            
            row.CreateCell(Config.Excel.SamplingSheet.Columns.Tray.Index).SetCellValue(description);

            CellRangeAddress plateDescriptionRange = new CellRangeAddress(_samplingSheet_rowIndex, _samplingSheet_rowIndex, 
                Config.Excel.SamplingSheet.Columns.Tray.Index, 
                Config.Excel.SamplingSheet.Columns.PCR.Index);
            _samplingSheet.AddMergedRegion(plateDescriptionRange);

            foreach (SampleGroup sg in plate.SampleGroups)
            {
                AddSampleGroup(sg);
            }
        }

        public void AddSampleGroup(SampleGroup sampleGroup)
        {
            _samplingSheet_rowIndex++;
            IRow row = _samplingSheet.CreateRow(_samplingSheet_rowIndex);

            row.CreateCell(Config.Excel.SamplingSheet.Columns.Tray.Index).SetCellValue(sampleGroup.Seeding.Tray.Name);
            string row_numbers;
            if (sampleGroup.Seeding.FromRow == sampleGroup.Seeding.ToRow)
            {
                row_numbers = Config.Excel.SamplingSheet.RowFormat;
                row_numbers = row_numbers.Replace("__ROW_NUMBER__", sampleGroup.Seeding.FromRow.ToString());
            }
            else
            {
                row_numbers = Config.Excel.SamplingSheet.RowsFormat;
                row_numbers = row_numbers.Replace("__FROM_ROW__", sampleGroup.Seeding.FromRow.ToString());
                row_numbers = row_numbers.Replace("__TO_ROW__", sampleGroup.Seeding.ToRow.ToString());
            }
            row.CreateCell(Config.Excel.SamplingSheet.Columns.Rows.Index).SetCellValue(row_numbers);
            row.CreateCell(Config.Excel.SamplingSheet.Columns.BagName.Index).SetCellValue(sampleGroup.Seeding.Bag.BagName);
            row.CreateCell(Config.Excel.SamplingSheet.Columns.Count.Index).SetCellValue(sampleGroup.Count);
            row.CreateCell(Config.Excel.SamplingSheet.Columns.PCR.Index).SetCellValue(string.Join(",", sampleGroup.Seeding.Bag.Samples));
        }

        private void Done()
        {
            _bagsSheet.AutoSizeColumn(Config.Excel.BagsSheet.Columns.BagName.Index);
            _bagsSheet.AutoSizeColumn(Config.Excel.BagsSheet.Columns.Samples.Index);
            _bagsSheet.AutoSizeColumn(Config.Excel.BagsSheet.Columns.Comment.Index);

            _seedingSheet.AutoSizeColumn(Config.Excel.SeedingSheet.Columns.BagName.Index);
            _seedingSheet.AutoSizeColumn(Config.Excel.SeedingSheet.Columns.PCR.Index);

            _samplingSheet.AutoSizeColumn(Config.Excel.SamplingSheet.Columns.Rows.Index);
            _samplingSheet.AutoSizeColumn(Config.Excel.SamplingSheet.Columns.BagName.Index);
            _samplingSheet.AutoSizeColumn(Config.Excel.SamplingSheet.Columns.PCR.Index);

        }

        public void SaveToFile(string filename)
        {
            Done();

            try
            {
                FileStream fileStream = new FileStream(filename, FileMode.Create);
                _workbook.Write(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
