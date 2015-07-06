using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Converter
{
    public class AppSettingsWrapper
    {
        public static String TigerInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerInputPath"]; }
        }

        public static String TigerOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TigerOutputPath"]; }
        }

        public static String TuebaInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaInputPath"]; }
        }

        public static String TuebaOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TuebaOutputPath"]; }
        }

        public static String HDTInputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HDTInputPath"]; }
        }

        public static String HDTOutputPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HDTOutputPath"]; }
        }
    }
}
