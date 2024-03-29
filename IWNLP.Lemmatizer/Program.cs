﻿using System;

namespace IWNLP.Lemmatizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 2) 
            {
                Lemmatizer lemmatizer = new Lemmatizer();
                String parsedWiktionary = args[0];
                String IWNLPExportPath = args[1];
                lemmatizer.CreateMapping(parsedWiktionary);
                lemmatizer.Save(IWNLPExportPath);
                if (args.Length == 3)
                {
                    lemmatizer.SaveJson(args[2]);
                }
            }
        }
    }
}
