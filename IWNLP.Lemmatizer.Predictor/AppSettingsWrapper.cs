using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Predictor
{
    public class AppSettingsWrapper
    {
        public static String TigerPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerPathUntagged"]; }
        }

        public static String TuebaPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaPathUntagged"]; }
        }

        public static String HdtPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HdtPathUntagged"]; }
        }

        public static String MateTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MateTueba"]; }
        }

        public static String MateHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MateHdt"]; }
        }

        public static String IWNLPTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPTiger"]; }
        }

        public static String IWNLPTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPTueba"]; }
        }

        public static String IWNLPHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPHdt"]; }
        }

        public static String TreeTaggerTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerTiger"]; }
        }

        public static String TreeTaggerTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerTueba"]; }
        }

        public static String TreeTaggerHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerHdt"]; }
        }

        public static String MorphyTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyTiger"]; }
        }

        public static String MorphyTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyTueba"]; }
        }

        public static String MorphyHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyHdt"]; }
        }

        public static String MorphyCSV
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyCSV"]; }
        }

        public static String IWNLPPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPPath"]; }
        }


        public class MateTools
        {
            public static String LemmatizerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.LemmatizerPath"]; }
            }

            public static String MorphTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.MorphTaggerPath"]; }
            }

            public static String PosTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.PosTaggerPath"]; }
            }

            public static String DepTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.DepTaggerPath"]; }
            }

            public static String TempPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.TempPath"]; }
            }
        }

        public class TreeTagger
        {
            public static String TreeTaggerExePath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerExePath"]; }
            }

            public static String TreeTaggerGermanPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerGermanPath"]; }
            }

            public static String TreeTaggerTempPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerTempPath"]; }
            }
        }
    }
}
