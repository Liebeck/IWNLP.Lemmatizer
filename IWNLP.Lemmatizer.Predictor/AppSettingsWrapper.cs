namespace IWNLP.Lemmatizer.Predictor
{
    public class AppSettingsWrapper
    {
        public static string TigerPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerPathUntagged"]; }
        }

        public static string TuebaPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaPathUntagged"]; }
        }

        public static string HdtPathUntagged
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HdtPathUntagged"]; }
        }

        public static string MateTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MateTueba"]; }
        }

        public static string MateHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MateHdt"]; }
        }

        public static string IWNLPTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPTiger"]; }
        }

        public static string IWNLPTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPTueba"]; }
        }

        public static string IWNLPHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPHdt"]; }
        }

        public static string TreeTaggerTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerTiger"]; }
        }

        public static string TreeTaggerTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerTueba"]; }
        }

        public static string TreeTaggerHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TreeTaggerHdt"]; }
        }

        public static string MorphyTiger
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyTiger"]; }
        }

        public static string MorphyTueba
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyTueba"]; }
        }

        public static string MorphyHdt
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyHdt"]; }
        }

        public static string MorphyCSV
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MorphyCSV"]; }
        }

        public static string IWNLPPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPPath"]; }
        }


        public class MateTools
        {
            public static string LemmatizerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.LemmatizerPath"]; }
            }

            public static string MorphTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.MorphTaggerPath"]; }
            }

            public static string PosTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.PosTaggerPath"]; }
            }

            public static string DepTaggerPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.DepTaggerPath"]; }
            }

            public static string TempPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["MateTools.TempPath"]; }
            }
        }

        public class TreeTagger
        {
            public static string TreeTaggerExePath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerExePath"]; }
            }

            public static string TreeTaggerGermanPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerGermanPath"]; }
            }

            public static string TreeTaggerTempPath
            {
                get { return System.Configuration.ConfigurationManager.AppSettings["TreeTagger.TreeTaggerTempPath"]; }
            }
        }
    }
}
