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

        public static string GetCellString(IRow row, int column_number)
        {
            string value = "";

            if (row != null)
            {
                ICell cell = row.GetCell(column_number);
                if (cell != null)
                {
                    if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                    {
                        value = cell.NumericCellValue.ToString();
                    }
                    else
                    {
                        value = cell.StringCellValue.Trim();
                    }
                }
            }

            return value;
        }

        public static int GetCellNumeric(IRow row, int column_number)
        {
            int value = 0;

            if (row != null)
            {
                ICell cell = row.GetCell(column_number);
                if (cell != null)
                {
                    value = Convert.ToInt32(cell.NumericCellValue);
                }
            }

            return value;
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

                // bag name is a must cell
                string bagName = GetCellString(bag_row, Config.Application.Excel.BagsSheet.Columns.BagName.Index);
                if (string.IsNullOrEmpty(bagName))
                {
                    // no bag name cell - exit
                    return false;
                }

                string fieldName = GetCellString(bag_row, Config.Application.Excel.BagsSheet.Columns.FieldName.Index);
                int seedsToPlant = GetCellNumeric(bag_row, Config.Application.Excel.BagsSheet.Columns.SeedsToPlant.Index);
                int seedsToSample = GetCellNumeric(bag_row, Config.Application.Excel.BagsSheet.Columns.SeedsToSample.Index);
                string samples = GetCellString(bag_row, Config.Application.Excel.BagsSheet.Columns.Samples.Index);
                string comment = GetCellString(bag_row, Config.Application.Excel.BagsSheet.Columns.Comment.Index);

                Bag bag = new Bag(bagName, fieldName, seedsToPlant, seedsToSample, samples, comment);
                _bags.Add(bag);
            }

            //Console.WriteLine("Found {0} bags in the file", _bags.Count);

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
