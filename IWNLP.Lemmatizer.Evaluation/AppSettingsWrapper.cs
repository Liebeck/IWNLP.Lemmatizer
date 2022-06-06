namespace IWNLP.Lemmatizer.Evaluation
{
    public class AppSettingsWrapper
    {
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


    }
}
