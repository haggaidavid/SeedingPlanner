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
        // configuration settings
        public class ColumnNumber
        {
            public static int FIELD_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["FieldColumnNumber"]);
            public static int BAG_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["BagNameColumnNumber"]);
            public static int SEEDS_TO_PLANT = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToPlantColumnNumber"]);
            public static int SEEDS_TO_SAMPLE = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToSampleColumnNumber"]);
            public static int SAMPLES = Convert.ToInt32(ConfigurationManager.AppSettings["SamplesColumnNumber"]);
            public static int COMMENT = Convert.ToInt32(ConfigurationManager.AppSettings["CommentColumnNumber"]);
        }

        private static List<Bag> _bags = new List<Bag>();
        public static int Count { get { return _bags.Count; } }

        /*
        public BagsInventory()
        {
            _bags = new List<Bag>();
        }
         */

        public static Bag GetAt(int index)
        {
            Bag bag = null;
            
            if (index < _bags.Count)
            {
                bag = _bags[index];
            }

            return bag;
        }

        public static bool LoadFromExcel(string filename)
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
                cell = bag_row.GetCell(ColumnNumber.FIELD_NAME);
                if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                {
                    fieldName = cell.NumericCellValue.ToString();
                }
                else
                {
                    fieldName = cell.StringCellValue;
                }

                string bagName = bag_row.GetCell(ColumnNumber.BAG_NAME).StringCellValue;
                int seedsToPlant = (int)bag_row.GetCell(ColumnNumber.SEEDS_TO_PLANT).NumericCellValue;
                int seedsToSample = (int)bag_row.GetCell(ColumnNumber.SEEDS_TO_SAMPLE).NumericCellValue;
                string samples = bag_row.GetCell(ColumnNumber.SAMPLES).StringCellValue;
                string comment = bag_row.GetCell(ColumnNumber.COMMENT).StringCellValue;

                Bag bag = new Bag(bagName, fieldName, seedsToPlant, seedsToSample, samples, comment);
                _bags.Add(bag);
            }

            return true;
        }

        public static bool SaveToExcel(string filename)
        {
            return false;
        }

        public static bool SaveToExcel(string filename, int[] ordering, bool bCalculateCost)
        {
            return false;
        }

    }
}
