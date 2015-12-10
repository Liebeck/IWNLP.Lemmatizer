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
        Dictionary<String, List<String>> morphyDictionary = new Dictionary<string, List<String>>();


        public void InitMorphy(String pathCSV)
        {
            List<String> lines = File.ReadAllLines(pathCSV).Where(x => !x.StartsWith("#") && !String.IsNullOrEmpty(x)).ToList();
            foreach (String line in lines)
            {
                string[] splitted = line.Split(new String[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (!morphyDictionary.ContainsKey(splitted[0]))
                {
                    morphyDictionary.Add(splitted[0], new List<String>());
                }
                morphyDictionary[splitted[0]].Add(splitted[1]);
            }
        }


        public void ProcessSentence(CoNLLSentence sentence)
        {
            string[] tokenArray = sentence.Tokens.Select(x => x.Form).ToArray();
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                CoNLLToken token = sentence.Tokens[i];
                if (token.POS == "NN")
                {
                    if (morphyDictionary.ContainsKey(token.Form))
                    {
                        token.PredictedLemmas = new List<string>();
                        token.PredictedLemmas = morphyDictionary[token.Form];
                    }
                    //else if (morphyDictionary.ContainsKey(token.Form.ToLower())) // adding a lower case comparison worsens the results
                    //{
                    //    token.PredictedLemmas = new List<string>();
                    //    token.PredictedLemmas = morphyDictionary[token.Form.ToLower()];
                    //}
                }
                else if (token.POS == "ADJA" || token.POS == "ADJD")
                {
                    if (morphyDictionary.ContainsKey(token.Form))
                    {
                        token.PredictedLemmas = new List<string>();
                        token.PredictedLemmas = morphyDictionary[token.Form];
                    }
                    else if (morphyDictionary.ContainsKey(token.Form.ToLower()))
                    {
                        token.PredictedLemmas = new List<string>();
                        token.PredictedLemmas = morphyDictionary[token.Form.ToLower()];
                    }
                }
                else if (token.POS.StartsWith("V"))
                {
                    if (morphyDictionary.ContainsKey(token.Form))
                    {
                        token.PredictedLemmas = new List<string>();
                        token.PredictedLemmas = morphyDictionary[token.Form];
                    }
                    else if (morphyDictionary.ContainsKey(token.Form.ToLower()))
                    {
                        token.PredictedLemmas = new List<string>();
                        token.PredictedLemmas = morphyDictionary[token.Form.ToLower()];
                    }
                }
            }
        }

    }
}
