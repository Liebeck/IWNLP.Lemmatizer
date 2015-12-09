using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWNLP.Models;

namespace IWNLP.Lemmatizer.Predictor
{
    public class IWNLPSentenceProcessor
    {
        public static void ProcessSentence(CoNLLSentence sentence, Lemmatizer iwnlp)
        {
            string[] tokenArray = sentence.Tokens.Select(x => x.Form).ToArray();
            //is2.data.SentenceData09 sentenceMateTools = mateToolsWrapper.TagSentenceLemmatizerAndPOS(tokenArray, true);
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                CoNLLToken token = sentence.Tokens[i];
                if (token.POS == "NN")
                {
                    List<POS> pos = new List<POS>() { POS.Noun, POS.X };
                    if (iwnlp.ContainsEntry(token.Form, POS.Noun))
                    {
                        token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Noun);
                    }
                    else if (iwnlp.ContainsEntry(token.Form, POS.X))
                    {
                        token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.X);
                    }
                    else if (iwnlp.ContainsEntry(token.Form, POS.AdjectivalDeclension))
                    {
                        token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.AdjectivalDeclension);
                    }
                    else if(iwnlp.ContainsEntry(token.Form, pos, true))
                    {
                        token.PredictedLemmas = iwnlp.GetLemmas(token.Form, pos, true);
                    }
                }
                else
                {
                    if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (iwnlp.ContainsEntry(token.Form, POS.Adjective))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Adjective);
                        }
                        else if (iwnlp.ContainsEntry(token.Form, POS.Adjective, true))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Adjective, true);
                        }
                        else if (iwnlp.ContainsEntry(token.Form, POS.Noun, true))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Noun, true);
                        }
                        else if (iwnlp.ContainsEntry(token.Form, POS.X, true))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.X, true);
                        }
                        else if (iwnlp.ContainsEntry(token.Form, POS.Verb, true))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Verb, true);
                        }
                        //else if (iwnlp.ContainsEntry(token.Form,true)) 
                        //{
                        //    token.PredictedLemmas = iwnlp.GetLemmas(token.Form, true);
                        //}
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        if (iwnlp.ContainsEntry(token.Form, POS.Verb, true))
                        {
                            token.PredictedLemmas = iwnlp.GetLemmas(token.Form, POS.Verb, true);
                        }
                            // test
                        //else if (iwnlp.ContainsEntry(token.Form, true))
                        //{
                        //    token.PredictedLemmas = iwnlp.GetLemmas(token.Form, true);
                        //}
                    }
                }
            }
        }
    }
}
