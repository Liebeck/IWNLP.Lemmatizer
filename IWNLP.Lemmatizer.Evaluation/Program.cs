using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Evaluation
{
    class Program
    {
        static void Main(string[] args)
        {

            //Lemmatizer a = new Lemmatizer();
            //a.CreateMapping(AppSettingsWrapper.IWNLPXMLPath);
            //a.Save(AppSettingsWrapper.IWNLPLemmatizerXMLPath);
            //Console.OutputEncoding = Encoding.UTF8;

            CorpusEvaluation evaluation = new CorpusEvaluation();
            var result1 = evaluation.Evaluate(AppSettingsWrapper.IWNLPTiger, "IWNLP - Tiger");
            //Console.WriteLine(result1.GetDetailedLookupInformation());
            var result2 = evaluation.Evaluate(AppSettingsWrapper.IWNLPTueba, "IWNLP - TüBa-D/Z");
            //Console.WriteLine(result2.GetDetailedLookupInformation());
            var result3 = evaluation.Evaluate(AppSettingsWrapper.IWNLPHdt, "IWNLP - HDT");
            //Console.WriteLine(result3.GetDetailedLookupInformation());
            Console.WriteLine("*******");
            //var result4 = evaluation.Evaluate(@"d:\\Datasets\\IWNLP\\Corpora\\hdt_tagged_IWNLP_20161020_a.xml", "IWNLP - Tiger");
            //Console.WriteLine(result4.GetDetailedLookupInformation());

            //evaluation.EvaluateWithKeep(AppSettingsWrapper.IWNLPTiger, "IWNLP + Keep: Tiger");
            //evaluation.EvaluateWithKeep(AppSettingsWrapper.IWNLPTueba, "IWNLP + Keep: Tüba-D/Z");
            //evaluation.EvaluateWithKeep(AppSettingsWrapper.IWNLPHdt, "IWNLP + Keep: HDT");

            //evaluation.Evaluate(AppSettingsWrapper.MorphyTiger, "Morphy - Tiger");
            //evaluation.Evaluate(AppSettingsWrapper.MorphyTueba, "Morphy - TüBa-D/Z");
            //evaluation.Evaluate(AppSettingsWrapper.MorphyHdt, "Morphy - HDT");

            //evaluation.EvaluateWithKeep(AppSettingsWrapper.MorphyTiger, "Morphy + Keep: Tiger");
            //evaluation.EvaluateWithKeep(AppSettingsWrapper.MorphyTueba, "Morphy + Keep: Tüba-D/Z");
            //evaluation.EvaluateWithKeep(AppSettingsWrapper.MorphyHdt, "Morphy + Keep: HDT");

            //Console.WriteLine("************************");
            //Console.WriteLine("Mate Tiger ---------");
            //evaluation.Evaluate(AppSettingsWrapper.MateTueba, "Mate Tools - TüBa-D/Z");
            //evaluation.Evaluate(AppSettingsWrapper.MateHdt, "Mate Tools - HDT");

            //evaluation.Evaluate(AppSettingsWrapper.TreeTaggerTiger, "TreeTagger - Tiger");
            //evaluation.Evaluate(AppSettingsWrapper.TreeTaggerTueba, "TreeTagger - TüBa-D/Z");
            //evaluation.Evaluate(AppSettingsWrapper.TreeTaggerHdt, "TreeTagger - HDT");

            //Console.WriteLine("************************");
            //Console.WriteLine("IWNLP + Mate Tools: Tiger - -----------");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.IWNLPTueba, AppSettingsWrapper.MateTueba, "IWNLP + Mate Tools: TüBa-D/Z");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.IWNLPHdt, AppSettingsWrapper.MateHdt, "IWNLP + Mate Tools: HDT");
            //Console.WriteLine("Morphy + Mate Tools: Tiger - -----------");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.MorphyTueba, AppSettingsWrapper.MateTueba, "Morphy + Mate Tools: TüBa-D/Z");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.MorphyHdt, AppSettingsWrapper.MateHdt, "Morphy + Mate Tools: HDT");

            //evaluation.EvaluateTwoResources(AppSettingsWrapper.IWNLPTiger, AppSettingsWrapper.TreeTaggerTiger, "IWNLP + TreeTagger: Tiger");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.IWNLPTueba, AppSettingsWrapper.TreeTaggerTueba, "IWNLP + TreeTagger: TüBa-D/Z");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.IWNLPHdt, AppSettingsWrapper.TreeTaggerHdt, "IWNLP + TreeTagger: HDT");

            //evaluation.EvaluateTwoResources(AppSettingsWrapper.MorphyTiger, AppSettingsWrapper.TreeTaggerTiger, "Morphy + TreeTagger: Tiger");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.MorphyTueba, AppSettingsWrapper.TreeTaggerTueba, "Morphy + TreeTagger: TüBa-D/Z");
            //evaluation.EvaluateTwoResources(AppSettingsWrapper.MorphyHdt, AppSettingsWrapper.TreeTaggerHdt, "Morphy + TreeTagger: HDT");

            Console.Beep(2000, 400);
            Console.Beep(2000, 400);
            Console.Beep(2000, 400);
            Console.WriteLine();
            Console.WriteLine("Evaluation completed");
            Console.ReadLine();
        }
    }
}
