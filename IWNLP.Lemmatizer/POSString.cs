using IWNLP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer
{
    public class LemmatizerItem
    {
        public POS POS { get; set; }
        public String Form { get; set; }
        public String Lemma { get; set; }


        public override bool Equals(object obj)
        {
            LemmatizerItem pos2 = (LemmatizerItem)obj;
            return this.POS == pos2.POS && this.Form == pos2.Form && this.Lemma == pos2.Lemma;
        }
    }
}
