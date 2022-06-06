using System.Collections.Generic;

namespace IWNLP.Lemmatizer.Models
{
    public class CoNLLSentence
    {
        public int ID { get; set; }
        public List<CoNLLToken> Tokens { get; set; }
    }
}
