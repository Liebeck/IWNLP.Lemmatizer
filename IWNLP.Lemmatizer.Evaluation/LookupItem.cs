using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Evaluation
{
    public class LookupItem
    {
        public String Form { get; set; }
        public String Lemma { get; set; }
        public List<String> PredictedLemma { get; set; }
    }
}
