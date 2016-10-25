using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2) 
            {
                Lemmatizer lemmatizer = new Lemmatizer();
                String parsedWiktionary = args[0];
                String IWNLPExportPath = args[1];
                lemmatizer.CreateMapping(parsedWiktionary);
                lemmatizer.Save(IWNLPExportPath);
            }
        }
    }
}
