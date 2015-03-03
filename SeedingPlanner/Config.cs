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
        public class ColumnNumber
        {
            // bags sheet
            public static int FIELD_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["FieldColumnNumber"]);
            public static int BAG_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["BagNameColumnNumber"]);
            public static int SEEDS_TO_PLANT = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToPlantColumnNumber"]);
            public static int SEEDS_TO_SAMPLE = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToSampleColumnNumber"]);
            public static int SAMPLES = Convert.ToInt32(ConfigurationManager.AppSettings["SamplesColumnNumber"]);
            public static int COMMENT = Convert.ToInt32(ConfigurationManager.AppSettings["CommentColumnNumber"]);

            // trays sheet
            public static int TRAY_DESCRIPTION = Convert.ToInt32(ConfigurationManager.AppSettings["TrayDescriptionColumnNumber"]);
            public static int BAG_NAME_IN_TRAY = Convert.ToInt32(ConfigurationManager.AppSettings["BagNameInTrayColumnNumber"]);
            public static int FROM_ROW = Convert.ToInt32(ConfigurationManager.AppSettings["FromRowColumnNumber"]);
            public static int TO_ROW = Convert.ToInt32(ConfigurationManager.AppSettings["ToRowColumnNumber"]);
        }

        public class ColumnName
        {
            public static string FIELD_NAME = ConfigurationManager.AppSettings["FieldColumnName"];
            public static string BAG_NAME = ConfigurationManager.AppSettings["BagNameColumnName"];
            public static string SEEDS_TO_PLANT = ConfigurationManager.AppSettings["SeedsToPlantColumnName"];
            public static string SEEDS_TO_SAMPLE = ConfigurationManager.AppSettings["SeedsToSampleColumnName"];
            public static string SAMPLES = ConfigurationManager.AppSettings["SamplesColumnName"];
            public static string COMMENT = ConfigurationManager.AppSettings["CommentColumnName"];

            public static string FROM_ROW_NAME = ConfigurationManager.AppSettings["FrowRowName"];
            public static string TO_ROW_NAME = ConfigurationManager.AppSettings["ToRowName"];
        }

        public class SheetFormat
        {
            public static string TRAY_DESCRIPTION_FORMAT = ConfigurationManager.AppSettings["TrayDescriptionFormat"];
        }
    }
}
