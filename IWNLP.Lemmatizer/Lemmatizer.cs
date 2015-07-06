using GenericXMLSerializer;
using IWNLP.Models;
using IWNLP.Models.Flections;
using IWNLP.Models.Nouns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWNLP.Lemmatizer
{
    public class Lemmatizer
    {
        protected Dictionary<String, List<String>> lemmaMapping;

        /// <summary>
        /// Use the parsed output from IWNLP to create the lemmatization mapping
        /// </summary>
        /// <param name="path">Path to the xml output of IWNLP</param>
        public void CreateMapping(String path)
        {
            List<Entry> deWiktionaryEntries = XMLSerializer.Deserialize<List<Entry>>(path);
            this.lemmaMapping = new Dictionary<string, List<string>>();

            //var verbCount = deWiktionaryEntries.Count(x => x is Verb);
            //var verbCount1 = deWiktionaryEntries.Count(x => x is Adjective);
            //var verbCount2 = deWiktionaryEntries.Count(x => x is VerbConjugation);
            //var verbCount3 = deWiktionaryEntries.Count(x => x is AdjectiveDeclination);
            //var verbCount4 = deWiktionaryEntries.Count(x => x is Noun);

            //var count3 = deWiktionaryEntries.Select(x => x.Text).Distinct().Count();
            for (int i = 0; i < deWiktionaryEntries.Count; i++)
            {
                if (deWiktionaryEntries[i].Text.StartsWith("Flexion:"))
                {
                    deWiktionaryEntries[i].Text = deWiktionaryEntries[i].Text.Substring("Flexion:".Length);
                }
            }

            //var count = deWiktionaryEntries.Select(x => x.Text).Distinct().Count();
            //var count2 = deWiktionaryEntries.Where(x => x.Text.Contains(":")).ToList();

            foreach (Entry entry in deWiktionaryEntries)
            {
                if (entry.Text.StartsWith("Flexion:"))
                {
                    entry.Text = entry.Text.Substring(8);
                }
                AddToDictionary(entry.Text, entry.Text);
                if (entry is Noun)
                {
                    Noun noun = (Noun)entry;
                    AddAllInflectionsToDictionary(noun.NominativSingular, noun.Text);
                    AddAllInflectionsToDictionary(noun.NominativPlural, noun.Text);
                    AddAllInflectionsToDictionary(noun.GenitivSingular, noun.Text);
                    AddAllInflectionsToDictionary(noun.GenitivPlural, noun.Text);
                    AddAllInflectionsToDictionary(noun.DativSingular, noun.Text);
                    AddAllInflectionsToDictionary(noun.DativPlural, noun.Text);
                    AddAllInflectionsToDictionary(noun.AkkusativSingular, noun.Text);
                    AddAllInflectionsToDictionary(noun.AkkusativPlural, noun.Text);
                }
                else if (entry is Verb)
                {
                    Verb verb = (Verb)entry;
                    AddFormsToDictionary(verb.Gegenwart_Ich, entry.Text);
                    AddFormsToDictionary(verb.Gegenwart_Du, entry.Text);
                    AddFormsToDictionary(verb.Gegenwart_ErSieEs, entry.Text);
                    AddFormsToDictionary(verb.Vergangenheit1_Ich, entry.Text);
                    AddFormsToDictionary(verb.KonjunktivII_Ich, entry.Text);

                    AddImperativeFormsToDictionary(verb.ImperativSingular, entry.Text);
                    AddImperativeFormsToDictionary(verb.ImperativPlural, entry.Text);
                    AddFormsToDictionary(verb.PartizipII, entry.Text);
                }
                else if (entry is Adjective)
                {
                    Adjective adjective = (Adjective)entry;
                    AddFormsToDictionary(adjective.Positiv, entry.Text);
                    AddFormsToDictionary(adjective.Komparativ, entry.Text);
                    AddFormsToDictionary(adjective.Superlativ, entry.Text);
                }
                else if (entry is Pronoun)
                {
                    Pronoun pronoun = (Pronoun)entry;
                    //AddFormsToDictionary(pronoun.WemEinzahlF, entry.Text);
                    //AddFormsToDictionary(pronoun.WemEinzahlM, entry.Text);
                    //AddFormsToDictionary(pronoun.WemEinzahlMehrzahl, entry.Text);
                    //AddFormsToDictionary(pronoun.WemEinzahlN, entry.Text);
                    //AddFormsToDictionary(pronoun.WenEinzahlF, entry.Text);
                    //AddFormsToDictionary(pronoun.WenEinzahlM, entry.Text);
                    //AddFormsToDictionary(pronoun.WenEinzahlMehrzahl, entry.Text);
                    //AddFormsToDictionary(pronoun.WenEinzahlN, entry.Text);
                    //AddFormsToDictionary(pronoun.WerEinzahlF, entry.Text);
                    //AddFormsToDictionary(pronoun.WerEinzahlM, entry.Text);
                    //AddFormsToDictionary(pronoun.WerEinzahlMehrzahl, entry.Text);
                    //AddFormsToDictionary(pronoun.WerEinzahlN, entry.Text);
                    //AddFormsToDictionary(pronoun.WessenEinzahlF, entry.Text);
                    //AddFormsToDictionary(pronoun.WessenEinzahlM, entry.Text);
                    //AddFormsToDictionary(pronoun.WessenEinzahlMehrzahl, entry.Text);
                    //AddFormsToDictionary(pronoun.WessenEinzahlN, entry.Text);

                }
                else if (entry is VerbConjugation)
                {
                    VerbConjugation verbConjugation = (VerbConjugation)entry;
                    AddToDictionary(verbConjugation.PartizipII, entry.Text);
                    if (!String.IsNullOrEmpty(verbConjugation.PartizipIIAlternativ))
                    {
                        AddToDictionary(verbConjugation.PartizipIIAlternativ, entry.Text);
                    }
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular1Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular2Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular3Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural1Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural2Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural3Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular1Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular2Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular3Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural1Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural2Person, entry.Text);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural3Person, entry.Text);
                    if (verbConjugation.PräsensAktivIndikativ_Singular1Person_Nebensatzkonjugation != null)
                    {
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular1Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular2Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular3Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural1Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural2Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural3Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular1Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular2Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular3Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural1Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural2Person_Nebensatzkonjugation, entry.Text);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural3Person_Nebensatzkonjugation, entry.Text);
                    }
                }
                else if (entry is AdjectiveDeclination)
                {
                    AdjectiveDeclination adjectiveDeclination = (AdjectiveDeclination)entry;
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativStark, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativSchwach, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativGemischt, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativStark, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativSchwach, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativGemischt, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativStark, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativStark, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativSchwach, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativSchwach, entry.Text);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativGemischt, entry.Text);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativGemischt, entry.Text);
                }
            }

            //var multiMappings = lemmaMapping.Where(x => x.Value.Count > 1).OrderByDescending(x => x.Value.Count()).ToList();
            //foreach (var kvp in multiMappings)
            //{
            //    if (kvp.Key.Length > 2)
            //    {
            //        if (kvp.Value.Where(x => x.Length > 2).Select(x => x.Substring(0, 3)).Distinct().Count() > 1)
            //        {
            //            //Console.WriteLine(kvp.Key + ": " + String.Join(",", kvp.Value.ToArray()));
            //        }
            //    }
            //}


            //var error1 = lemmaMapping.Where(x => x.Value.Any(y => y.Contains("/")));
            //var error2 = lemmaMapping.Where(x => x.Value.Any(y => y.Contains("ich ") || y.Contains("du ") || y.Contains("er/sie/es") || y.Contains("wir ")));
            //var error3 = lemmaMapping.Where(x => x.Value.Any(y => y.Contains("(") || y.Contains(")")));
            //var error4 = lemmaMapping.Where(x => x.Value.Any(y => y.Contains(",")));
            //var error5 = lemmaMapping.Where(x => x.Value.Any(y => String.IsNullOrEmpty(y)));
            //var error6 = lemmaMapping.Where(x => x.Value.Any(y => y.Contains("''")));

            //var error11 = lemmaMapping.Where(x => x.Key.Contains("/"));
            //var error12 = lemmaMapping.Where(x => x.Key.Contains("ich ") || x.Key.Contains("du ") || x.Key.Contains("er/sie/es") || x.Key.Contains("wir "));
            //var error13 = lemmaMapping.Where(x => x.Key.Contains("(") || x.Key.Contains(")"));
            //var error14 = lemmaMapping.Where(x => x.Key.Contains(","));
            //var error15 = lemmaMapping.Where(x => String.IsNullOrEmpty(x.Key));
            //var error16 = lemmaMapping.Where(x => x.Key.Contains("''"));

        }

        protected void AddToDictionary(String key, String value)
        {
            if (lemmaMapping.ContainsKey(key))
            {
                if (!lemmaMapping[key].Contains(value))
                {
                    lemmaMapping[key].Add(value);
                }
            }
            else
            {
                lemmaMapping[key] = new List<string>();
                lemmaMapping[key].Add(value);
            }
        }

        void AddAllInflectionsToDictionary(List<Inflection> inflections, String word)
        {
            foreach (Inflection inflection in inflections)
            {
                AddToDictionary(inflection.InflectedWord, word);
            }
        }

        void AddFormsToDictionary(List<String> forms, String word)
        {
            foreach (String form in forms)
            {
                AddToDictionary(form, word);
            }
        }

        void AddImperativeFormsToDictionary(List<String> forms, String word)
        {
            foreach (String imperativ in forms)
            {
                if (imperativ.EndsWith("!"))
                {
                    AddToDictionary(imperativ.Substring(0, imperativ.Length - 1), word);
                }
                else
                {
                    AddToDictionary(imperativ, word);
                }

            }
        }

        /// <summary>
        /// Saves IWNLP to the specified path into a XML file
        /// </summary>
        /// <param name="path"></param>
        public void Save(String path)
        {
            List<WordForm> forms = this.lemmaMapping.Select(x => new WordForm()
            {
                Form = x.Key,
                Lemmas = x.Value
            }).ToList();
            XMLSerializer.Serialize<List<WordForm>>(forms, path);
        }

        /// <summary>
        /// Loads IWNLP from the XML file
        /// </summary>
        /// <param name="path"></param>
        public void Load(String path)
        {
            List<WordForm> forms = XMLSerializer.Deserialize<List<WordForm>>(path);
            this.lemmaMapping = forms.ToDictionary(x => x.Form, x => x.Lemmas);
        }

        /// <summary>
        /// Return if the given word form is present in IWNLP
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool ContainsEntry(String word)
        {
            return this.lemmaMapping.ContainsKey(word);
        }

        /// <summary>
        /// Return all lemmas for a given inflected form
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<String> GetLemmas(String word)
        {
            return this.lemmaMapping[word];
        }
    }
}
