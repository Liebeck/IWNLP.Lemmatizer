using GenericXMLSerializer;
using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class CorpusEvaluation
    {

        public void NormalizeVerbToken(CoNLLToken token)
        {
            if (token.Lemma.Contains("%aux"))
            {
                token.Lemma = token.Lemma.Replace("%aux", String.Empty);
            }
            if (token.Lemma.Contains("%passiv"))
            {
                token.Lemma = token.Lemma.Replace("%passiv", String.Empty);
            }
        }

        public void Evaluate(String path, String comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);

            int nounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN");
            int verbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V"));
            int adjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "ADJA" || x.POS == "ADJD");


            //Dictionary<String, int> wrongMappings = new Dictionary<string, int>();
            int nounCorrectLemmatizedCount = 0;
            int verbCorrectLemmatizedCount = 0;
            int adjectiveCorrectLemmatizedCount = 0;

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            nounCorrectLemmatizedCount++;
                        }
                        //else 
                        //{
                        //    if (token.PredictedLemmas != null && token.PredictedLemmas.Count == 1)
                        //    {
                        //        String key = String.Format("{0}->{1} != {2}", token.Form, token.PredictedLemmas[0], token.Lemma);
                        //        if (!wrongMappings.ContainsKey(key))
                        //        {
                        //            wrongMappings.Add(key, 0);
                        //        }
                        //        wrongMappings[key] = wrongMappings[key] + 1;
                        //    }                        
                        //}
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        NormalizeVerbToken(token);
                        if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            verbCorrectLemmatizedCount++;
                        }
                        //else 
                        //{
                        //    if (token.PredictedLemmas != null && token.PredictedLemmas.Count == 1)
                        //    {
                        //        String key = String.Format("{0}->{1} != {2}", token.Form, token.PredictedLemmas[0], token.Lemma);
                        //        if (!wrongMappings.ContainsKey(key))
                        //        {
                        //            wrongMappings.Add(key, 0);
                        //        }
                        //        wrongMappings[key] = wrongMappings[key] + 1;
                        //    }                        
                        //}
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            adjectiveCorrectLemmatizedCount++;
                        }
                        //else
                        //{
                        //    if (token.PredictedLemmas != null && token.PredictedLemmas.Count == 1)
                        //    {
                        //        String key = String.Format("{0}->{1} != {2}", token.Form, token.PredictedLemmas[0], token.Lemma);
                        //        if (!wrongMappings.ContainsKey(key))
                        //        {
                        //            wrongMappings.Add(key, 0);
                        //        }
                        //        wrongMappings[key] = wrongMappings[key] + 1;
                        //    }
                        //}
                    }
                }
            }

            //var wrongMappingSorted = wrongMappings.OrderByDescending(x => x.Value);


            double nounPercent = ((double)nounCorrectLemmatizedCount) / nounCount;
            double verbPercent = ((double)verbCorrectLemmatizedCount) / verbCount;
            double adjectivePercent = ((double)adjectiveCorrectLemmatizedCount) / adjectiveCount;

            Console.WriteLine(String.Format("Nouns: {0}/{1} = {2}", nounCorrectLemmatizedCount, nounCount, String.Format("{0:0.000}", nounPercent)));
            Console.WriteLine(String.Format("Verbs: {0}/{1} = {2}", verbCorrectLemmatizedCount, verbCount, String.Format("{0:0.000}", verbPercent)));
            Console.WriteLine(String.Format("Adjectives: {0}/{1} = {2}", adjectiveCorrectLemmatizedCount, adjectiveCount, String.Format("{0:0.000}", adjectivePercent)));
            Console.WriteLine("");
        }

        public void EvaluateWithKeep(String path, String comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);

            int nounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN");
            int verbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V"));
            int adjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "ADJA" || x.POS == "ADJD");

            int nounCorrectLemmatizedCount = 0;
            int verbCorrectLemmatizedCount = 0;
            int adjectiveCorrectLemmatizedCount = 0;

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    nounCorrectLemmatizedCount++;
                                }
                            }
                            else // more than one form, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    nounCorrectLemmatizedCount++;
                                }
                            }
                        }
                        else // if no entry is found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                nounCorrectLemmatizedCount++;
                            }
                        }
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        NormalizeVerbToken(token);
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    verbCorrectLemmatizedCount++;
                                }
                            }
                            else // more than one form found, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    verbCorrectLemmatizedCount++;
                                }
                            }
                        }
                        else // no entry found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                verbCorrectLemmatizedCount++;
                            }
                        }
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    adjectiveCorrectLemmatizedCount++;
                                }
                            }
                            else // more than one form found, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    adjectiveCorrectLemmatizedCount++;
                                }
                            }

                        }
                        else // no entry found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                adjectiveCorrectLemmatizedCount++;
                            }
                        }
                    }
                }
            }


            double nounPercent = ((double)nounCorrectLemmatizedCount) / nounCount;
            double verbPercent = ((double)verbCorrectLemmatizedCount) / verbCount;
            double adjectivePercent = ((double)adjectiveCorrectLemmatizedCount) / adjectiveCount;

            Console.WriteLine(String.Format("Nouns: {0}/{1} = {2}", nounCorrectLemmatizedCount, nounCount, String.Format("{0:0.000}", nounPercent)));
            Console.WriteLine(String.Format("Verbs: {0}/{1} = {2}", verbCorrectLemmatizedCount, verbCount, String.Format("{0:0.000}", verbPercent)));
            Console.WriteLine(String.Format("Adjectives: {0}/{1} = {2}", adjectiveCorrectLemmatizedCount, adjectiveCount, String.Format("{0:0.000}", adjectivePercent)));
            Console.WriteLine("");
        }

        protected bool IsExactMatch(String goldLemma, List<String> lemmas)
        {
            if (lemmas == null || lemmas.Count == 0)
            {
                return false;
            }
            if (lemmas.Count > 1)
            {
                return false;
            }
            else
            {
                return goldLemma == lemmas[0];
            }
        }

        //protected bool IsLowerCaseExactMatch(String goldLemma, List<String> lemmas)
        //{
        //    if (lemmas == null || lemmas.Count == 0)
        //    {
        //        return false;
        //    }
        //    if (lemmas.Count > 1)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return goldLemma.ToLower() == lemmas[0].ToLower();
        //    }
        //}

        //protected bool IsExactMatchOrGuess(String goldLemma, String form, List<String> lemmas)
        //{
        //    if (lemmas == null || lemmas.Count == 0)
        //    {
        //        return (form == goldLemma);
        //    }
        //    if (lemmas.Count > 1)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return goldLemma.ToLower() == lemmas[0].ToLower();
        //    }
        //}



        public void EvaluateTwoResources(String path, String path2, String comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);
            List<CoNLLSentence> sentences2 = XMLSerializer.Deserialize<List<CoNLLSentence>>(path2);

            int nounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN");
            int verbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V"));
            int adjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "ADJA" || x.POS == "ADJD");

            int nounCorrectLemmatizedCount = 0;
            int verbCorrectLemmatizedCount = 0;
            int adjectiveCorrectLemmatizedCount = 0;

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    nounCorrectLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    nounCorrectLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                nounCorrectLemmatizedCount++;
                            }
                        }
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        NormalizeVerbToken(token);
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    verbCorrectLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    verbCorrectLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                verbCorrectLemmatizedCount++;
                            }
                        }
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    adjectiveCorrectLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    adjectiveCorrectLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                adjectiveCorrectLemmatizedCount++;
                            }
                        }
                    }
                }
            }

            double nounPercent = ((double)nounCorrectLemmatizedCount) / nounCount;
            double verbPercent = ((double)verbCorrectLemmatizedCount) / verbCount;
            double adjectivePercent = ((double)adjectiveCorrectLemmatizedCount) / adjectiveCount;

            Console.WriteLine(String.Format("Nouns: {0}/{1} = {2}", nounCorrectLemmatizedCount, nounCount, String.Format("{0:0.000}", nounPercent)));
            Console.WriteLine(String.Format("Verbs: {0}/{1} = {2}", verbCorrectLemmatizedCount, verbCount, String.Format("{0:0.000}", verbPercent)));
            Console.WriteLine(String.Format("Adjectives: {0}/{1} = {2}", adjectiveCorrectLemmatizedCount, adjectiveCount, String.Format("{0:0.000}", adjectivePercent)));
            Console.WriteLine("");
        }

    }
}
