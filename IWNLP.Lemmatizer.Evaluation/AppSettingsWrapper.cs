using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class AppSettingsWrapper
    {
        public static String IWNLPXMLPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPXMLPath"]; }
        }

        public static String IWNLPLemmatizerXMLPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IWNLPLemmatizerXMLPath"]; }
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


    }
}
