using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace SeedingPlanner
{
    public class Config
    {
        private static Config _instance;
        private Config() { }

        public static Config Application
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Config();
                    _instance.Load();
                }
                return _instance;
            }
        }

        public class TrayConfig
        {
            public int NumberOfRows = 17;
            public int NumberOfCellsInRow = 11;
        }
        public TrayConfig Tray = new TrayConfig();

        public class PlateConfig
        {
            public int NumberOfSamples = 92;
        }
        public PlateConfig Plate = new PlateConfig();

        public class ExcelConfig
        {
            public class ColumnInfo
            {
                public string Name;
                public int Index;
            }

            public class BagsSheetConfig
            {
                public class ColumnsConfig
                {
                    public ColumnInfo FieldName = new ColumnInfo { Name = "Field name", Index = 1 };
                    public ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 2 };
                    public ColumnInfo SeedsToPlant = new ColumnInfo { Name = "ToPlant", Index = 3 };
                    public ColumnInfo SeedsToSample = new ColumnInfo { Name = "ToSample", Index = 4 };
                    public ColumnInfo Samples = new ColumnInfo { Name = "Samples", Index = 5 };
                    public ColumnInfo Comment = new ColumnInfo { Name = "Comment", Index = 6 };
                }
                public ColumnsConfig Columns = new ColumnsConfig();

                public string SheetName = "Seed Bags";
            }
            public BagsSheetConfig BagsSheet = new BagsSheetConfig();

            public class SeedingSheetConfig
            {
                public class ColumnsConfig
                {
                    public ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 1 };
                    public ColumnInfo FromRow = new ColumnInfo { Name = "From", Index = 2 };
                    public ColumnInfo ToRow = new ColumnInfo { Name = "To", Index = 3 };
                    public ColumnInfo SeedsToSample = new ColumnInfo { Name = "ToSample", Index = 4 };
                    public ColumnInfo PCR = new ColumnInfo { Name = "Samples", Index = 5 };
                }
                public ColumnsConfig Columns = new ColumnsConfig();

                public string SheetName = "Seeding";
                public string DescriptionFormat = "מגש מספר __TRAY_NUMBER__, סה'כ __SEEDS_COUNT__ זרעים";
            }
            public SeedingSheetConfig SeedingSheet = new SeedingSheetConfig();

            public class SamplingSheetConfig
            {
                public class ColumnsConfig
                {
                    public ColumnInfo Tray = new ColumnInfo { Name = "Tray", Index = 1 };
                    public ColumnInfo Rows = new ColumnInfo { Name = "Rows", Index = 2 };
                    public ColumnInfo BagName = new ColumnInfo { Name = "Bag Name", Index = 3 };
                    public ColumnInfo Count = new ColumnInfo { Name = "Count", Index = 4 };
                    public ColumnInfo PCR = new ColumnInfo { Name = "PCR", Index = 5 };
                }
                public ColumnsConfig Columns = new ColumnsConfig();

                public string SheetName = "Sampling";
                public string DecriptionFormat = "פלטה __PLATE_NAME__, __SEEDS_COUNT__ זרעים מתוך __TRAYS_COUNT__ מגשים, דגימות: __SAMPLES__";
                public string RowFormat = "שורה __ROW_NUMBER__";
                public string RowsFormat = "שורות __FROM_ROW__-__TO_ROW__";
            }
            public SamplingSheetConfig SamplingSheet = new SamplingSheetConfig();
        }
        public ExcelConfig Excel = new ExcelConfig();
        public void Init()
        {
        }

        public void Save(string filename = "settings.json")
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public void Load(string filename = "settings.json")
        {
            string json = File.ReadAllText(filename);
            // TODO: hope this one overide also static values
            Config c = JsonConvert.DeserializeObject<Config>(json);
        }

    }
}
