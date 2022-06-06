using IWNLP.Lemmatizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.AmbiguousNouns = new List<LookupItem>();
            this.WrongVerbs = new List<LookupItem>();
            this.MissedVerbs = new List<LookupItem>();
            this.AmbiguousVerbs = new List<LookupItem>();
            this.WrongAdjectives = new List<LookupItem>();
            this.MissedAdjectives = new List<LookupItem>();
            this.AmbiguousAdjectives = new List<LookupItem>();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Nouns: {0}/{1} = {2}", this.NounsCorrectlyLemmatizedCount, this.TotalNounCount, string.Format("{0:0.000}", this.NounPercent)));
            stringBuilder.AppendLine(string.Format("Verbs: {0}/{1} = {2}", this.VerbsCorrectlyLemmatizedCount, this.TotalVerbCount, string.Format("{0:0.000}", this.VerbPercent)));
            stringBuilder.AppendLine(string.Format("Adjectives: {0}/{1} = {2}", this.AdjectivesCorrectlyLemmatizedCount, this.TotalAdjectiveCount, string.Format("{0:0.000}", this.AdjectivePercent)));
            return stringBuilder.ToString();
        }

        public void AddWrongLookup(PartOfSpeech pos, string form, string lemma, List<string> predictedLemma)
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

        public void AddMissingLookup(PartOfSpeech pos, string form, string lemma, List<string> predictedLemma)
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

        public void AddAmbiguousLookup(PartOfSpeech pos, string form, string lemma, List<string> predictedLemma)
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
                this.AddMissingLookup(pos, token.Form, token.Lemma, token.PredictedLemmas);
            }
            else
            {
                if (token.PredictedLemmas.Count == 1)
                {
                    this.AddWrongLookup(pos, token.Form, token.Lemma, token.PredictedLemmas);
                }
                else
                {
                    this.AddAmbiguousLookup(pos, token.Form, token.Lemma, token.PredictedLemmas);
                }
            }
        }

        public string GetDetailedLookupInformation() 
        {
            int topCount = 30;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("** Missed nouns **");
            stringBuilder.Append(GetTopEntries(MissedNouns, topCount));
            stringBuilder.AppendLine("** Missed verbs **");
            stringBuilder.Append(GetTopEntries(MissedVerbs, topCount));
            stringBuilder.AppendLine("** Missed adjectives **");
            stringBuilder.Append(GetTopEntries(MissedAdjectives, topCount));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("** Wrong nouns **");
            stringBuilder.Append(GetTopEntries(WrongNouns, topCount));
            stringBuilder.AppendLine("** Wrong verbs **");
            stringBuilder.Append(GetTopEntries(WrongVerbs, topCount));
            stringBuilder.AppendLine("** Wrong adjectives **");
            stringBuilder.Append(GetTopEntries(WrongAdjectives, topCount));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("** Ambiguous nouns **");
            stringBuilder.Append(GetTopEntries(AmbiguousNouns, topCount));
            stringBuilder.AppendLine("** Ambiguous verbs **");
            stringBuilder.Append(GetTopEntries(AmbiguousVerbs, topCount));
            stringBuilder.AppendLine("** Ambiguous adjectives **");
            stringBuilder.Append(GetTopEntries(AmbiguousAdjectives, topCount));
            return stringBuilder.ToString();
        }

        protected string GetTopEntries(List<LookupItem> list, int count) 
        {
            StringBuilder stringBuilder = new StringBuilder();
            var topList = list.GroupBy(x => new Tuple<string, string, string>(x.Form, x.Lemma, (x.PredictedLemma != null) ? string.Join(",", x.PredictedLemma.ToArray()) : string.Empty))
                            .OrderByDescending(x => x.Count())
                            .Take(count);
            foreach (var item in topList) 
            {
                stringBuilder.AppendLine(string.Format("{0}: {1}->{2} !={3}", item.Count(), item.Key.Item1, item.Key.Item2, item.Key.Item3));
            }
            return stringBuilder.ToString();
        }
    }

    public enum PartOfSpeech 
    { 
        Noun,
        Verb,
        Adjective
    }
}
