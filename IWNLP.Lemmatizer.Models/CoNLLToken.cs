using System.Collections.Generic;

namespace IWNLP.Lemmatizer.Models
{
    public class CoNLLToken
    {
        public string ID { get; set; }
        public string Form { get; set; }
        public string Lemma { get; set; }
        public string POS { get; set; }
        public List<string> PredictedLemmas { get; set; }

    }
}
