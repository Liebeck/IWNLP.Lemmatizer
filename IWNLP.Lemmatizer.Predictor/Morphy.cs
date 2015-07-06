using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Predictor
{
    public class Morphy
    {
        Dictionary<String, String> morphyDictionary = new Dictionary<string, string>();

        public void InitMorphy(String pathCSV)
        {
            List<String> lines = File.ReadAllLines(pathCSV).Where(x => !x.StartsWith("#") && !String.IsNullOrEmpty(x)).ToList();
            foreach (String line in lines)
            {
                string[] splitted = line.Split(new String[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (!morphyDictionary.ContainsKey(splitted[0]))
                {
                    morphyDictionary.Add(splitted[0], splitted[1]);
                }
            }
        }

        public void ProcessSentence(CoNLLSentence sentence)
        {
            string[] tokenArray = sentence.Tokens.Select(x => x.Form).ToArray();
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                CoNLLToken token = sentence.Tokens[i];
                if(morphyDictionary.ContainsKey(token.Form))
                {
                    token.PredictedLemmas = new List<string>();
                    token.PredictedLemmas.Add(morphyDictionary[token.Form]);
                }
            }
        }

    }
}
