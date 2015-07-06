using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Predictor
{
    public class MateTools
    {
        MateToolsWrapper mateToolsWrapper;
        public void InitMateTools()
        {
            Stopwatch stopwatchInit = new Stopwatch();
            stopwatchInit.Start();
            mateToolsWrapper = new MateToolsWrapper(new WrapperOptions(
                AppSettingsWrapper.MateTools.LemmatizerPath,
                AppSettingsWrapper.MateTools.MorphTaggerPath,
                AppSettingsWrapper.MateTools.PosTaggerPath,
                AppSettingsWrapper.MateTools.DepTaggerPath,
                AppSettingsWrapper.MateTools.TempPath));
            mateToolsWrapper.Initialize();

            long initTime = stopwatchInit.ElapsedMilliseconds / 1000;
            stopwatchInit.Stop();
        }


        public void ProcessSentence(CoNLLSentence sentence)
        {
            string[] tokenArray = sentence.Tokens.Select(x => x.Form).ToArray();
            is2.data.SentenceData09 sentenceMateTools = mateToolsWrapper.TagSentenceLemmatizerAndPOS(tokenArray, true);
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                String mateToolsLemma = sentenceMateTools.plemmas[i + 1]; // zero based index is ROOT node
                sentence.Tokens[i].PredictedLemmas = new List<string>();
                sentence.Tokens[i].PredictedLemmas.Add(mateToolsLemma);
            }

        }
    }
}
