using GenericXMLSerializer;
using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Predictor
{
    class Program
    {


        static void Main(string[] args)
        {
            TreeTagger treeTagger = new TreeTagger();
            //MateTools mate = new MateTools();
            //mate.InitMateTools();

            List<CoNLLSentence> tiger = XMLSerializer.Deserialize<List<CoNLLSentence>>(AppSettingsWrapper.TigerPathUntagged);
            List<CoNLLSentence> tueba = XMLSerializer.Deserialize<List<CoNLLSentence>>(AppSettingsWrapper.TuebaPathUntagged);
            List<CoNLLSentence> hdt = XMLSerializer.Deserialize<List<CoNLLSentence>>(AppSettingsWrapper.HdtPathUntagged);

            LemmatizeTreeTagger(tiger, AppSettingsWrapper.TreeTaggerTiger, treeTagger);
            LemmatizeTreeTagger(tueba, AppSettingsWrapper.TreeTaggerTueba, treeTagger);
            LemmatizeTreeTagger(hdt, AppSettingsWrapper.TreeTaggerHdt, treeTagger);

            LemmatizeIWNLP(tiger, AppSettingsWrapper.IWNLPTiger);
            LemmatizeIWNLP(tueba, AppSettingsWrapper.IWNLPTueba);
            LemmatizeIWNLP(hdt, AppSettingsWrapper.IWNLPHdt);

            LemmatizeMorphy(tiger, AppSettingsWrapper.MorphyTiger);
            LemmatizeMorphy(tueba, AppSettingsWrapper.MorphyTueba);
            LemmatizeMorphy(hdt, AppSettingsWrapper.MorphyHdt);

            //LemmatizeMate(tueba, AppSettingsWrapper.MateTueba, mate);
            //LemmatizeMate(hdt, AppSettingsWrapper.MateHdt, mate);

        }

        static void LemmatizeMate(List<CoNLLSentence> corpus, String exportPath, MateTools mateTools)
        {
            int count = corpus.Count;
            for (int i = 0; i < count; i++)
            {
                CoNLLSentence sentence = corpus[i];
                mateTools.ProcessSentence(sentence);
                Console.WriteLine(i);
            }
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, exportPath);

        }

        static void LemmatizeTreeTagger(List<CoNLLSentence> corpus, String exportPath, TreeTagger treeTagger)
        {
            int count = corpus.Count;
            for (int j = 0; j < count; j++)
            {
                CoNLLSentence sentence = corpus[j];
                treeTagger.ProcessSentence(sentence);
                Console.WriteLine(j);
            }
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, exportPath);


        }

        static void LemmatizeIWNLP(List<CoNLLSentence> corpus, String exportPath)
        {
            Lemmatizer IWNLP = new Lemmatizer();
            IWNLP.Load(AppSettingsWrapper.IWNLPPath);

            int count = corpus.Count;
            for (int i = 0; i < count; i++)
            {
                CoNLLSentence sentence = corpus[i];
                IWNLPSentenceProcessor.ProcessSentence(sentence, IWNLP);
                //Console.WriteLine(i);
            }
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, exportPath);

        }

        static void LemmatizeMorphy(List<CoNLLSentence> corpus, String exportPath)
        {
            Morphy morphy = new Morphy();
            morphy.InitMorphy(AppSettingsWrapper.MorphyCSV);
            int count = corpus.Count;
            for (int i = 0; i < count; i++)
            {
                CoNLLSentence sentence = corpus[i];
                morphy.ProcessSentence(sentence);
                Console.WriteLine(i);
            }
            XMLSerializer.Serialize<List<CoNLLSentence>>(corpus, exportPath);

        }

    }
}
