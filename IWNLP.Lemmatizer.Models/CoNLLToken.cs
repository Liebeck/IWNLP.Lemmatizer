using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Models
{
    public class CoNLLToken
    {
        public String ID { get; set; }
        public String Form { get; set; }
        public String Lemma { get; set; }
        public String POS { get; set; }
        public List<String> PredictedLemmas { get; set; }

    }
}
