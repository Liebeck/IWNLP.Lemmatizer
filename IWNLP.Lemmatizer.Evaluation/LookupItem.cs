using System.Collections.Generic;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class LookupItem
    {
        public string Form { get; set; }
        public string Lemma { get; set; }
        public List<string> PredictedLemma { get; set; }
    }
}
