using System.Collections.Generic;

namespace IWNLP.Lemmatizer
{
    /// <summary>
    /// wrapperclass for the C# serialization of a dictionary
    /// </summary>
    public class WordForm
    {
        public string Form { get; set; }
        public List<LemmatizerItem> Lemmas { get; set; }
    }
}
