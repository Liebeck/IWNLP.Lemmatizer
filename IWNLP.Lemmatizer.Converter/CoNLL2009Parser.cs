using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;

namespace IWNLP.Lemmatizer.Converter
{
    public class CoNLL2009Parser
    {
        public List<CoNLLSentence> ReadFile(string path, Corpus corpus)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            List<CoNLLSentence> sentences = new List<CoNLLSentence>();

            int sentenceNumber = 1;
            CoNLLSentence nextSentence = new CoNLLSentence();
            nextSentence.ID = sentenceNumber++;
            nextSentence.Tokens = new List<CoNLLToken>();
            string line;
            
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#")) 
                {
                    continue;
                }
                if (string.IsNullOrEmpty(line))
                {
                    sentences.Add(nextSentence);
                    nextSentence = new CoNLLSentence();
                    nextSentence.ID = sentenceNumber++;
                    nextSentence.Tokens = new List<CoNLLToken>();
                }
                else
                {
                    string[] values = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (corpus == Corpus.Tiger || corpus == Corpus.HDT)
                    {
                        nextSentence.Tokens.Add(new CoNLLToken()
                        {
                            ID = values[0],
                            Form = values[1],
                            Lemma = values[2],
                            POS = values[4],
                        });
                    }
                    else 
                    {
                        string lemma = values[6];
                        if (lemma == "#refl") 
                        {
                            lemma = string.Empty;
                        }
                        if (lemma.Contains("#")) 
                        {
                            lemma = lemma.Replace("#", string.Empty);
                        }
                        nextSentence.Tokens.Add(new CoNLLToken()
                        {
                            ID = values[2],
                            Form = values[3],
                            Lemma = lemma,
                            POS = values[4],
                        });                        
                    }

                }
            }
            return sentences;
        }
    }
}
