using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Converter
{
    public class CoNLL2009Parser
    {
        public List<CoNLLSentence> ReadFile(String path, Corpus corpus)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            List<CoNLLSentence> sentences = new List<CoNLLSentence>();

            int sentenceNumber = 1;
            CoNLLSentence nextSentence = new CoNLLSentence();
            nextSentence.ID = sentenceNumber++;
            nextSentence.Tokens = new List<CoNLLToken>();
            String line;
            
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#")) 
                {
                    continue;
                }
                if (String.IsNullOrEmpty(line))
                {
                    sentences.Add(nextSentence);
                    nextSentence = new CoNLLSentence();
                    nextSentence.ID = sentenceNumber++;
                    nextSentence.Tokens = new List<CoNLLToken>();
                }
                else
                {
                    String[] values = line.Split(new String[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
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
                        String lemma = values[6];
                        if (lemma == "#refl") 
                        {
                            lemma = String.Empty;
                        }
                        if (lemma.Contains("#")) 
                        {
                            lemma = lemma.Replace("#", String.Empty);
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
