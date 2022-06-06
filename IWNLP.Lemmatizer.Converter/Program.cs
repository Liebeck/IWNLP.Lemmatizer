using IWNLP.Lemmatizer.Models;
using System.Collections.Generic;

namespace IWNLP.Lemmatizer.Converter
{
    public class Program
    {
        static void Main(string[] args)
        {
            CoNLL2009Parser parser = new CoNLL2009Parser();
            List<CoNLLSentence> corpus = null;

            corpus = parser.ReadFile(AppSettingsWrapper.TigerInputPath, Corpus.Tiger);
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, AppSettingsWrapper.TigerOutputPath);

            corpus = parser.ReadFile(AppSettingsWrapper.TuebaInputPath, Corpus.TuebaDZ);
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, AppSettingsWrapper.TuebaOutputPath);

            corpus = parser.ReadFile(AppSettingsWrapper.HDTInputPath, Corpus.HDT);
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, AppSettingsWrapper.HDTOutputPath);
        }
    }
}
