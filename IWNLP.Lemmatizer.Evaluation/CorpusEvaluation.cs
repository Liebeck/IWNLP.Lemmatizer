using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class CorpusEvaluation
    {

        public void NormalizeVerbToken(CoNLLToken token)
        {
            if (token.Lemma.Contains("%aux"))
            {
                token.Lemma = token.Lemma.Replace("%aux", string.Empty);
            }
            if (token.Lemma.Contains("%passiv"))
            {
                token.Lemma = token.Lemma.Replace("%passiv", string.Empty);
            }
        }


        public DetailedLookupResults Evaluate(string path, string comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);

            DetailedLookupResults result = new DetailedLookupResults()
            {
                TotalNounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN" && !x.Lemma.Contains("|") && !x.Lemma.Contains("_") && (x.Lemma != "unknown" && x.Form != "unknown")),
                TotalVerbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V")),
                TotalAdjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => (x.POS == "ADJA" || x.POS == "ADJD") && (x.Lemma != "NULL" && x.Form != "NULL"))// the second condition is for tokens in the HDT corpus that have the lemma "NULL"
            };

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (token.Lemma.Contains("|") || token.Lemma.Contains("_") || (token.Lemma=="unknown" && token.Form!="unknown")) { continue; }
                        if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            result.NounsCorrectlyLemmatizedCount++;
                        }
                        else
                        {
                            result.AddLookup(PartOfSpeech.Noun, token);
                        }
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        NormalizeVerbToken(token);
                        if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            result.VerbsCorrectlyLemmatizedCount++;
                        }
                        else
                        {
                            result.AddLookup(PartOfSpeech.Verb, token);
                        }
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (token.Lemma == "NULL" && token.Form != "NULL")  // ~ 2000 adjectives in the HDT corpus have "NULL" as lemma. Ignore them for the evaluation
                        {
                            continue;
                        }
                        if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                        {
                            result.AdjectivesCorrectlyLemmatizedCount++;
                        }
                        else
                        {
                            result.AddLookup(PartOfSpeech.Adjective, token);
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

            //foreach (var entry in wrongMappingSorted.Take(100))
            //{
            //    Console.WriteLine(entry.Key + ": " + entry.Value);
            //}

            Console.WriteLine(result.ToString());
            return result;
        }

        public DetailedLookupResults EvaluateWithKeep(string path, string comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);

            DetailedLookupResults result = new DetailedLookupResults()
            {
                TotalNounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN" && !x.Lemma.Contains("|") && !x.Lemma.Contains("_") && (x.Lemma != "unknown" && x.Form != "unknown")),
                TotalVerbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V")),
                TotalAdjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => (x.POS == "ADJA" || x.POS == "ADJD") && (x.Lemma != "NULL" && x.Form != "NULL"))// the second condition is for tokens in the HDT corpus that have the lemma "NULL"
            };

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (token.Lemma.Contains("|") || token.Lemma.Contains("_") || (token.Lemma == "unknown" && token.Form != "unknown")) { continue; }
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.NounsCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Noun, token.Form, token.Lemma, token.PredictedLemmas); }
                            }
                            else // more than one form, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    result.NounsCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Noun, token.Form, token.Lemma, new List<string>() { token.Form }); }
                            }
                        }
                        else // if no entry is found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                result.NounsCorrectlyLemmatizedCount++;
                            }
                            else { result.AddWrongLookup(PartOfSpeech.Noun, token.Form, token.Lemma, new List<string>() { token.Form }); }
                        }
                    }
                    else if (token.POS.StartsWith("V"))
                    {
                        NormalizeVerbToken(token);
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.VerbsCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Verb, token.Form, token.Lemma, token.PredictedLemmas); }
                            }
                            else // more than one form found, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    result.VerbsCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Verb, token.Form, token.Lemma, new List<string>() { token.Form }); }
                            }
                        }
                        else // no entry found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                result.VerbsCorrectlyLemmatizedCount++;
                            }
                            else { result.AddWrongLookup(PartOfSpeech.Verb, token.Form, token.Lemma, new List<string>() { token.Form }); }
                        }
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (token.Lemma == "NULL" && token.Form != "NULL") // ~ 2000 adjectives in the HDT corpus have "NULL" as lemma. Ignore them for the evaluation
                        {
                            continue;
                        }
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.AdjectivesCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Adjective, token.Form, token.Lemma, token.PredictedLemmas); }
                            }
                            else // more than one form found, use "keep" approach
                            {
                                if (token.Lemma == token.Form)
                                {
                                    result.AdjectivesCorrectlyLemmatizedCount++;
                                }
                                else { result.AddWrongLookup(PartOfSpeech.Adjective, token.Form, token.Lemma, new List<string>() { token.Form }); }
                            }

                        }
                        else // no entry found, use the "keep" approach
                        {
                            if (token.Lemma == token.Form)
                            {
                                result.AdjectivesCorrectlyLemmatizedCount++;
                            }
                            else { result.AddWrongLookup(PartOfSpeech.Adjective, token.Form, token.Lemma, new List<string>() { token.Form }); }
                        }
                    }
                }
            }
            Console.WriteLine(result.ToString());
            return result;
        }

        //protected bool IsExactMatch(String goldLemma, List<String> lemmas)
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
        //        return goldLemma == lemmas[0];
        //    }
        //}

        protected bool IsLowerCaseExactMatch(string goldLemma, List<string> lemmas)
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
                return goldLemma.ToLower() == lemmas[0].ToLower();
            }
        }

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



        public DetailedLookupResults EvaluateTwoResources(string path, string path2, string comment)
        {
            Console.WriteLine(comment);
            List<CoNLLSentence> sentences = XMLSerializer.Deserialize<List<CoNLLSentence>>(path);
            List<CoNLLSentence> sentences2 = XMLSerializer.Deserialize<List<CoNLLSentence>>(path2);

            DetailedLookupResults result = new DetailedLookupResults()
            {
                TotalNounCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS == "NN" && !x.Lemma.Contains("|") && !x.Lemma.Contains("_") && (x.Lemma != "unknown" && x.Form != "unknown")),
                TotalVerbCount = sentences.SelectMany(x => x.Tokens).Count(x => x.POS.StartsWith("V")),
                TotalAdjectiveCount = sentences.SelectMany(x => x.Tokens).Count(x => (x.POS == "ADJA" || x.POS == "ADJD") && (x.Lemma != "NULL" && x.Form != "NULL"))// the second condition is for tokens in the HDT corpus that have the lemma "NULL"
            };

            for (int i = 0; i < sentences.Count; i++)
            {
                CoNLLSentence sentence = sentences[i];
                for (int j = 0; j < sentence.Tokens.Count; j++)
                {
                    CoNLLToken token = sentence.Tokens[j];
                    if (token.POS == "NN")
                    {
                        if (token.Lemma.Contains("|") || token.Lemma.Contains("_") || (token.Lemma == "unknown" && token.Form != "unknown")) { continue; }
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.NounsCorrectlyLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    result.NounsCorrectlyLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                result.NounsCorrectlyLemmatizedCount++;
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
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.VerbsCorrectlyLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    result.VerbsCorrectlyLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                result.VerbsCorrectlyLemmatizedCount++;
                            }
                        }
                    }
                    else if (token.POS == "ADJA" || token.POS == "ADJD")
                    {
                        if (token.Lemma == "NULL" && token.Form != "NULL") // ~ 2000 adjectives in the HDT corpus have "NULL" as lemma. Ignore them for the evaluation
                        {
                            continue;
                        }
                        if (!(token.PredictedLemmas == null || token.PredictedLemmas.Count == 0))
                        {
                            if (token.PredictedLemmas.Count == 1)
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, token.PredictedLemmas))
                                {
                                    result.AdjectivesCorrectlyLemmatizedCount++;
                                }
                            }
                            else // if more than one lemma is found, compare with the second resource
                            {
                                if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                                {
                                    result.AdjectivesCorrectlyLemmatizedCount++;
                                }
                            }
                        }
                        else // if no lemma is found, compare with the second resource
                        {
                            if (IsLowerCaseExactMatch(token.Lemma, sentences2[i].Tokens[j].PredictedLemmas))
                            {
                                result.AdjectivesCorrectlyLemmatizedCount++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(result.ToString());
            return result;
        }

    }
}
