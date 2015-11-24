using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer
{
    /// <summary>
    /// wrapperclass for the C# serialization of a dictionary
    /// </summary>
    public class WordForm
    {
        public String Form { get; set; }
        public List<LemmatizerItem> Lemmas { get; set; }
    }
}
