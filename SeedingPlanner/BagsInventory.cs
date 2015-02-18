using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace SeedingPlanner
{
    class BagsInventory
    {
        private List<Bag> _bags;

        private int _fieldName_ColumnNumber;
        private int _bagName_ColumnNumber;
        private int _seedsToPlant_ColumnNumber;
        private int _seedsToSample_ColumnNumber;
        private int _samples_ColumnNumber;
        private int _comment_ColumnNumber;

        public BagsInventory()
        {
            _bags = new List<Bag>();

            // initialize column numbers
            var appSettings = ConfigurationManager.AppSettings;
            _fieldName_ColumnNumber = Convert.ToInt32(appSettings["FieldColumnNumber"]);
            _bagName_ColumnNumber = Convert.ToInt32(appSettings["BagNameColumnNumber"]);
            _seedsToPlant_ColumnNumber = Convert.ToInt32(appSettings["SeedsToPlantColumnNumber"]);
            _seedsToSample_ColumnNumber = Convert.ToInt32(appSettings["SeedsToSampleColumnNumber"]);
            _samples_ColumnNumber = Convert.ToInt32(appSettings["SamplesColumnNumber"]);
            _comment_ColumnNumber = Convert.ToInt32(appSettings["CommentColumnNumber"]);

        }

        public Bag GetAt(int index)
        {
            Bag bag = null;
            
            if (index < _bags.Count)
            {
                bag = _bags[index];
            }

            return bag;
        }

        public bool LoadFromExcel(string filename)
        {
            IWorkbook workbook = null;
            if (filename.EndsWith(".xlsx"))
            {
                workbook = new XSSFWorkbook(new FileStream(filename, FileMode.Open, FileAccess.Read));
            }
            else if (filename.EndsWith(".xls"))
            {
                workbook = new HSSFWorkbook(new FileStream(filename, FileMode.Open, FileAccess.Read));
            }
            else
            {
                return false;
            }

            ISheet sheet = workbook.GetSheetAt(0);
            IRow bag_row = null;
            for (int row_number = 1; null != (bag_row = sheet.GetRow(row_number)); ++row_number)
            {
                ICell cell = null;

                string fieldName;
                cell = bag_row.GetCell(_fieldName_ColumnNumber);
                if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                {
                    fieldName = cell.NumericCellValue.ToString();
                }
                else
                {
                    fieldName = cell.StringCellValue;
                }

                string bagName = bag_row.GetCell(_bagName_ColumnNumber).StringCellValue;
                int seedsToPlant = (int)bag_row.GetCell(_seedsToPlant_ColumnNumber).NumericCellValue;
                int seedsToSample = (int)bag_row.GetCell(_seedsToSample_ColumnNumber).NumericCellValue;
                string samples = bag_row.GetCell(_samples_ColumnNumber).StringCellValue;
                string comment = bag_row.GetCell(_comment_ColumnNumber).StringCellValue;

                Bag bag = new Bag(bagName, fieldName, seedsToPlant, seedsToSample, samples, comment);
                _bags.Add(bag);
            }

            return true;
        }

        public bool SaveToExcel(string filename)
        {
            return false;
        }

        public bool SaveToExcel(string filename, int[] ordering)
        {
            return false;
        }
    }
}
