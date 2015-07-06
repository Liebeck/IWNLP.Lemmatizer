using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Predictor
{
    public class IWNLPSentenceProcessor
    {
        public static void ProcessSentence(CoNLLSentence sentence, Lemmatizer IWNLP)
        {
            string[] tokenArray = sentence.Tokens.Select(x => x.Form).ToArray();
            //is2.data.SentenceData09 sentenceMateTools = mateToolsWrapper.TagSentenceLemmatizerAndPOS(tokenArray, true);
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                CoNLLToken token = sentence.Tokens[i];
                if (IWNLP.ContainsEntry(token.Form)) 
                {
                    token.PredictedLemmas = IWNLP.GetLemmas(token.Form);
                }
            }
        }
    }
}
