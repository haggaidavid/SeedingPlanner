using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Windows.Forms;

namespace SeedingPlanner
{
    class BagsInventory
    {
        // configuration settings

        private static List<Bag> _bags = new List<Bag>();
        public static int Count { get { return _bags.Count; } }

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
            try
            {
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
            }
            catch (IOException ex)
            {
                MessageBox.Show("error: " + ex.Message);
                return false;
            }

            ISheet sheet = workbook.GetSheetAt(0);
            IRow bag_row = null;
            for (int row_number = 1; null != (bag_row = sheet.GetRow(row_number)); ++row_number)
            {
                ICell cell = null;

                string fieldName;
                cell = bag_row.GetCell(Config.ColumnNumber.FIELD_NAME);
                if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                {
                    fieldName = cell.NumericCellValue.ToString();
                }
                else
                {
                    fieldName = cell.StringCellValue.Trim();
                }

                string bagName = bag_row.GetCell(Config.ColumnNumber.BAG_NAME).StringCellValue.Trim();
                int seedsToPlant = (int)bag_row.GetCell(Config.ColumnNumber.SEEDS_TO_PLANT).NumericCellValue;
                int seedsToSample = (int)bag_row.GetCell(Config.ColumnNumber.SEEDS_TO_SAMPLE).NumericCellValue;
                string samples = bag_row.GetCell(Config.ColumnNumber.SAMPLES).StringCellValue.Trim();
                string comment = bag_row.GetCell(Config.ColumnNumber.COMMENT).StringCellValue.Trim();

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
