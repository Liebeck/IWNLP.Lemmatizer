using IWNLP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IWNLP.Lemmatizer
{
    public class LemmatizerItem
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public POS POS { get; set; }
        public string Form { get; set; }
        public string Lemma { get; set; }


        public override bool Equals(object obj)
        {
            LemmatizerItem pos2 = (LemmatizerItem)obj;
            return this.POS == pos2.POS && this.Form == pos2.Form && this.Lemma == pos2.Lemma;
        }
    }
}
