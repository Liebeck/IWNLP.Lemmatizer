using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer.Models
{
    public class CoNLLSentence
    {
        public int ID { get; set; }
        public List<CoNLLToken> Tokens { get; set; }
    }
}
