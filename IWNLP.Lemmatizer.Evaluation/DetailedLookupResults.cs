using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class DetailedLookupResults
    {
        public List<LookupItem> WrongNouns { get; set; }
        public List<LookupItem> MissedNouns { get; set; }
        public List<LookupItem> AmbiguousNouns {get;set;}

        public List<LookupItem> WrongVerbs { get; set; }
        public List<LookupItem> MissedVerbs { get; set; }
        public List<LookupItem> AmbiguousVerbs { get; set; }

        public List<LookupItem> WrongAdjectives { get; set; }
        public List<LookupItem> MissedAdjectives { get; set; }
        public List<LookupItem> AmbiguousAdjectives { get; set; }

        public int TotalNounCount { get; set; }
        public int TotalVerbCount { get; set; }
        public int TotalAdjectiveCount { get; set; }

        public int NounsCorrectlyLemmatizedCount { get; set; }
        public int VerbsCorrectlyLemmatizedCount { get; set; }
        public int AdjectivesCorrectlyLemmatizedCount { get; set; }

        public double NounPercent
        {
            get { return ((double)NounsCorrectlyLemmatizedCount) / TotalNounCount; }
        }

        public double VerbPercent
        {
            get { return ((double)VerbsCorrectlyLemmatizedCount) / TotalVerbCount; }
        }

        public double AdjectivePercent
        {
            get { return ((double)AdjectivesCorrectlyLemmatizedCount) / TotalAdjectiveCount; }
        }

        public DetailedLookupResults()
        {
            this.WrongNouns = new List<LookupItem>();
            this.MissedNouns = new List<LookupItem>();
            this.WrongVerbs = new List<LookupItem>();
            this.MissedVerbs = new List<LookupItem>();
            this.WrongAdjectives = new List<LookupItem>();
            this.MissedAdjectives = new List<LookupItem>();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(String.Format("Nouns: {0}/{1} = {2}", this.NounsCorrectlyLemmatizedCount, this.TotalNounCount, String.Format("{0:0.000}", this.NounPercent)));
            stringBuilder.AppendLine(String.Format("Verbs: {0}/{1} = {2}", this.VerbsCorrectlyLemmatizedCount, this.TotalVerbCount, String.Format("{0:0.000}", this.VerbPercent)));
            stringBuilder.AppendLine(String.Format("Adjectives: {0}/{1} = {2}", this.AdjectivesCorrectlyLemmatizedCount, this.TotalAdjectiveCount, String.Format("{0:0.000}", this.AdjectivePercent)));
            return stringBuilder.ToString();
        }

        public void AddWrongLookup(PartOfSpeech pos, String form, String lemma, List<String> predictedLemma)
        {
            switch (pos) 
            {
                case PartOfSpeech.Noun:
                    this.WrongNouns.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Verb:
                    this.WrongVerbs.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Adjective:
                    this.WrongAdjectives.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
            }
        }

        public void AddMissingLookup(PartOfSpeech pos, String form, String lemma, List<String> predictedLemma)
        {
            switch (pos)
            {
                case PartOfSpeech.Noun:
                    this.MissedNouns.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Verb:
                    this.MissedVerbs.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Adjective:
                    this.MissedAdjectives.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
            }
        }

        public void AddAmbiguousLookup(PartOfSpeech pos, String form, String lemma, List<String> predictedLemma)
        {
            switch (pos)
            {
                case PartOfSpeech.Noun:
                    this.AmbiguousNouns.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Verb:
                    this.AmbiguousVerbs.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
                case PartOfSpeech.Adjective:
                    this.AmbiguousAdjectives.Add(new LookupItem() { Form = form, Lemma = lemma, PredictedLemma = predictedLemma });
                    break;
            }
        }

        public void AddLookup(PartOfSpeech pos, CoNLLToken token) 
        {
            if (token.PredictedLemmas == null || (token.PredictedLemmas != null && token.PredictedLemmas.Count == 0))
            {
                this.AddMissingLookup(PartOfSpeech.Noun, token.Form, token.POS, token.PredictedLemmas);
            }
            else
            {
                if (token.PredictedLemmas.Count == 1)
                {
                    this.AddWrongLookup(PartOfSpeech.Noun, token.Form, token.POS, token.PredictedLemmas);
                }
                else
                {
                    this.AddAmbiguousLookup(PartOfSpeech.Noun, token.Form, token.POS, token.PredictedLemmas);
                }
            }
        }
    }

    public enum PartOfSpeech 
    { 
        Noun,
        Verb,
        Adjective
    }
}
