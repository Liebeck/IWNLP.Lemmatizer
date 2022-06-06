namespace IWNLP.Lemmatizer.Converter
{
    public class AppSettingsWrapper
    {
        public static string TigerInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerInputPath"]; }
        }

        public static string TigerOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerOutputPath"]; }
        }

        public static string TuebaInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaInputPath"]; }
        }

        public static string TuebaOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaOutputPath"]; }
        }

        public static string HDTInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HDTInputPath"]; }
        }

        public static string HDTOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HDTOutputPath"]; }
        }
    }
}
