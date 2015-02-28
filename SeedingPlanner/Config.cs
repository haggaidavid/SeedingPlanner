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
            public static int FIELD_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["FieldColumnNumber"]);
            public static int BAG_NAME = Convert.ToInt32(ConfigurationManager.AppSettings["BagNameColumnNumber"]);
            public static int SEEDS_TO_PLANT = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToPlantColumnNumber"]);
            public static int SEEDS_TO_SAMPLE = Convert.ToInt32(ConfigurationManager.AppSettings["SeedsToSampleColumnNumber"]);
            public static int SAMPLES = Convert.ToInt32(ConfigurationManager.AppSettings["SamplesColumnNumber"]);
            public static int COMMENT = Convert.ToInt32(ConfigurationManager.AppSettings["CommentColumnNumber"]);
        }

        public class ColumnName
        {
            public static string FIELD_NAME = ConfigurationManager.AppSettings["FieldColumnName"];
            public static string BAG_NAME = ConfigurationManager.AppSettings["BagNameColumnName"];
            public static string SEEDS_TO_PLANT = ConfigurationManager.AppSettings["SeedsToPlantColumnName"];
            public static string SEEDS_TO_SAMPLE = ConfigurationManager.AppSettings["SeedsToSampleColumnName"];
            public static string SAMPLES = ConfigurationManager.AppSettings["SamplesColumnName"];
            public static string COMMENT = ConfigurationManager.AppSettings["CommentColumnName"];
        }

    }
}
