using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SeedingPlanner
{
    class Config
    {
        public class Tray
        {
            public static int NumberOfRows = 17;
            public static int NumberOfCellsInRow = 11;
        }

        public class Plate
        {
            public static int NumberOfSamples = 92;
        }

        public class Excel
        {
            public class ColumnInfo
            {
                public string Name;
                public int Index;
            }

            public class BagsSheet
            {
                public class Columns
                {
                    public static ColumnInfo FieldName = new ColumnInfo { Name = "Field name", Index = 1 };
                    public static ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 2 };
                    public static ColumnInfo SeedsToPlant = new ColumnInfo { Name = "ToPlant", Index = 3 };
                    public static ColumnInfo SeedsToSample = new ColumnInfo { Name = "ToSample", Index = 4 };
                    public static ColumnInfo Samples = new ColumnInfo { Name = "Samples", Index = 5 };
                    public static ColumnInfo Comment = new ColumnInfo { Name = "Comment", Index = 6 };
                }

                public static string SheetName = "Seed Bags";
            }

            public class SeedingSheet
            {
                public class Columns
                {
                    public static ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 1 };
                    public static ColumnInfo FromRow = new ColumnInfo { Name = "From", Index = 2 };
                    public static ColumnInfo ToRow = new ColumnInfo { Name = "To", Index = 3 };
                    public static ColumnInfo SeedsToSample = new ColumnInfo { Name = "ToSample", Index = 4 };
                    public static ColumnInfo PCR = new ColumnInfo { Name = "Samples", Index = 5 };
                }

                public static string SheetName = "Seeding";
                public static string DescriptionFormat = "מגש מספר __TRAY_NUMBER__, סה'כ __SEEDS_COUNT__ זרעים";
            }

            public class SamplingSheet
            {
                public class Columns
                {
                    public static ColumnInfo Tray = new ColumnInfo { Name = "Tray", Index = 1 };
                    public static ColumnInfo Rows = new ColumnInfo { Name = "Rows", Index = 2 };
                    public static ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 3 };
                    public static ColumnInfo Count = new ColumnInfo { Name = "Count", Index = 4 };
                    public static ColumnInfo PCR = new ColumnInfo { Name = "PCR", Index = 5 };
                }

                public static string SheetName = "Sampling";
                public static string DecriptionFormat = "פלטה __PLATE_NAME__, __SEEDS_COUNT__ זרעים מתוך __TRAYS_COUNT__ מגשים, דגימות: __SAMPLES__";
                public static string RowFormat = "שורה __ROW_NUMBER__";
                public static string RowsFormat = "שורות __FROM_ROW__-__TO_ROW__";
            }
        }

        public void Init()
        {
        }

        public void Save(string filename = "settings.json")
        {

        }

        public void Load(string filename = "settings.json")
        {

        }

    }
}
