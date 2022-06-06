using IWNLP.Lemmatizer.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IWNLP.Lemmatizer.Predictor
{
    public class TreeTagger
    {
        string inputPath = AppSettingsWrapper.TreeTagger.TreeTaggerTempPath + "input.txt";
        string outputPath = AppSettingsWrapper.TreeTagger.TreeTaggerTempPath + "output.txt";

        public void ProcessSentence(CoNLLSentence sentence)
        {
            StringBuilder tokenString = new StringBuilder();
            foreach (CoNLLToken token in sentence.Tokens)
            {
                tokenString.AppendLine(string.Format("{0}", token.Form));
            }

            System.IO.File.WriteAllText(inputPath, tokenString.ToString(), Encoding.UTF8);

            //german-utf8.par input.txt output.txt -token -lemma 
            Process treeTaggerProcess = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {

                    FileName = AppSettingsWrapper.TreeTagger.TreeTaggerExePath,
                    Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" -token -lemma",
                            AppSettingsWrapper.TreeTagger.TreeTaggerGermanPath,
                            inputPath,
                            outputPath),
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true 

                }
            };
            treeTaggerProcess.Start();

            // the new process runs asynchronous. Wait until it stops to read its output afterwards
            treeTaggerProcess.WaitForExit();

            string[] allLinesOuputFile = System.IO.File.ReadAllLines(outputPath, Encoding.UTF8);
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                string[] line = allLinesOuputFile[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length == 3 && line[2] != "<unknown>")
                {
                    sentence.Tokens[i].PredictedLemmas = line[2].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    // TreeTagger can return multiple lemmas
                    // Example: Stiften	NN	Stift|Stiften
                    // For instance "Stiften" will return "Stift" and "Stiften".
                }
            }
        }
    }
}
