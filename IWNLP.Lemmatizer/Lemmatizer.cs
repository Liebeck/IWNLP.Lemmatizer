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
        //protected Dictionary<String, List<String>> lemmaMapping;
        protected Dictionary<String, List<LemmatizerItem>> lemmaMapping;

        /// <summary>
        /// Use the parsed output from IWNLP to create the lemmatization mapping
        /// </summary>
        /// <param name="path">Path to the xml output of IWNLP</param>
        public void CreateMapping(String path)
        {
            List<Entry> deWiktionaryEntries = XMLSerializer.Deserialize<List<Entry>>(path);
            //this.lemmaMapping = new Dictionary<string, List<string>>();
            this.lemmaMapping = new Dictionary<string, List<LemmatizerItem>>();

            //var verbCount = deWiktionaryEntries.Count(x => x is Verb);
            //var verbCount1 = deWiktionaryEntries.Count(x => x is Adjective);
            //var verbCount2 = deWiktionaryEntries.Count(x => x is VerbConjugation);
            //var verbCount3 = deWiktionaryEntries.Count(x => x is AdjectiveDeclination);
            //var verbCount4 = deWiktionaryEntries.Count(x => x is Noun);

            //var count3 = deWiktionaryEntries.Select(x => x.Text).Distinct().Count();
            //for (int i = 0; i < deWiktionaryEntries.Count; i++)
            //{
            //    if (deWiktionaryEntries[i].Text.StartsWith("Flexion:"))
            //    {
            //        deWiktionaryEntries[i].Text = deWiktionaryEntries[i].Text.Substring("Flexion:".Length);
            //    }
            //}

            //var count = deWiktionaryEntries.Select(x => x.Text).Distinct().Count();
            //var count2 = deWiktionaryEntries.Where(x => x.Text.Contains(":")).ToList();

            foreach (Entry entry in deWiktionaryEntries)
            {
                if (entry.Text.StartsWith("Flexion:"))
                {
                    entry.Text = entry.Text.Substring(8);
                }
                //AddToDictionary(entry.Text, entry.Text);
                if (entry is Noun)
                {
                    AddToDictionary(entry.Text, entry.Text, POS.Noun);
                    Noun noun = (Noun)entry;
                    AddAllInflectionsToDictionary(noun.NominativSingular, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.NominativPlural, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.GenitivSingular, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.GenitivPlural, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.DativSingular, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.DativPlural, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.AkkusativSingular, noun.Text, POS.Noun);
                    AddAllInflectionsToDictionary(noun.AkkusativPlural, noun.Text, POS.Noun);
                }
                else if (entry is Verb)
                {
                    AddToDictionary(entry.Text, entry.Text, POS.Verb);
                    Verb verb = (Verb)entry;
                    AddFormsToDictionary(verb.Präsens_Ich, entry.Text, POS.Verb);
                    AddFormsToDictionary(verb.Präsens_Du, entry.Text, POS.Verb);
                    AddFormsToDictionary(verb.Präsens_ErSieEs, entry.Text, POS.Verb);
                    AddFormsToDictionary(verb.Präteritum_ich, entry.Text, POS.Verb);
                    AddFormsToDictionary(verb.KonjunktivII_Ich, entry.Text, POS.Verb);

                    AddImperativeFormsToDictionary(verb.ImperativSingular, entry.Text);
                    AddImperativeFormsToDictionary(verb.ImperativPlural, entry.Text);
                    AddFormsToDictionary(verb.PartizipII, entry.Text, POS.Verb);
                }
                else if (entry is Adjective)
                {
                    AddToDictionary(entry.Text, entry.Text, POS.Adjective);
                    Adjective adjective = (Adjective)entry;
                    AddFormsToDictionary(adjective.Positiv, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjective.Komparativ, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjective.Superlativ, entry.Text, POS.Adjective);
                }
                else if (entry is Pronoun)
                {
                    AddToDictionary(entry.Text, entry.Text, POS.Pronoun);
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
                    AddToDictionary(entry.Text, entry.Text, POS.Verb);
                    VerbConjugation verbConjugation = (VerbConjugation)entry;
                    AddToDictionary(verbConjugation.PartizipII, entry.Text, POS.Verb);
                    if (!String.IsNullOrEmpty(verbConjugation.PartizipIIAlternativ))
                    {
                        AddToDictionary(verbConjugation.PartizipIIAlternativ, entry.Text, POS.Verb);
                    }
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular1Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular2Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular3Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural1Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural2Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural3Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular1Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular2Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular3Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural1Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural2Person, entry.Text, POS.Verb);
                    AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural3Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular1Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular2Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular3Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural1Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural2Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural3Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular1Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular2Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular3Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural1Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural2Person, entry.Text, POS.Verb);
                    //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural3Person, entry.Text, POS.Verb);

                    if (verbConjugation.PräsensAktivIndikativ_Singular1Person_Nebensatzkonjugation != null)
                    {
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Singular3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräsensAktivIndikativ_Plural3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Singular3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        AddFormsToDictionary(verbConjugation.PräteritumAktivIndikativ_Plural3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Singular3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräsensAktivKonjunktiv_Plural3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Singular3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural1Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural2Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                        //AddFormsToDictionary(verbConjugation.PräteritumAktivKonjunktiv_Plural3Person_Nebensatzkonjugation, entry.Text, POS.Verb);
                    }
                }
                else if (entry is AdjectivalDeclension)
                {
                    AdjectivalDeclension adjectivalDeclension = (AdjectivalDeclension)entry;
                    AddFormsToDictionary(adjectivalDeclension.NominativSingular, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.GenitivSingular, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.DativSingular, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.AkkusativSingular, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.NominativPlural, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.GenitivPlural, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.DativPlural, entry.Text, POS.AdjectivalDeclension);
                    AddFormsToDictionary(adjectivalDeclension.AkkusativPlural, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.NominativSingularSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.GenitivSingularSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.DativSingularSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.AkkusativPluralSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.NominativPluralSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.GenitivPluralSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.DativPluralSchwach, entry.Text, POS.AdjectivalDeclension);
                    AddAllInflectionsToDictionary(adjectivalDeclension.AkkusativPluralSchwach, entry.Text, POS.AdjectivalDeclension);
                }
                else if (entry is AdjectiveDeclination)
                {
                    #region Adjective
                    AddToDictionary(entry.Text, entry.Text, POS.Adjective);
                    AdjectiveDeclination adjectiveDeclination = (AdjectiveDeclination)entry;
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativStark, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativSchwach, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivMaskulinumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivFemininumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivNeutrumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.PositivPluralAkkusativGemischt, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativStark, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativSchwach, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativMaskulinumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativFemininumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativNeutrumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.KomparativPluralAkkusativGemischt, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativStark, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativStark, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativSchwach, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativSchwach, entry.Text, POS.Adjective);

                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativMaskulinumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativFemininumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativNeutrumAkkusativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralNominativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralGenitivGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralDativGemischt, entry.Text, POS.Adjective);
                    AddFormsToDictionary(adjectiveDeclination.SuperlativPluralAkkusativGemischt, entry.Text, POS.Adjective);
                    #endregion
                }
                else
                {
                    AddToDictionary(entry.Text, entry.Text, POS.X);
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

        protected void AddToDictionary(String key, String lemma, POS pos)
        {
            LemmatizerItem val = new LemmatizerItem() { POS = pos, Form = key, Lemma = lemma };
            key = key.ToLower();
            if (lemmaMapping.ContainsKey(key))
            {
                if (!lemmaMapping[key].Contains(val))
                {
                    lemmaMapping[key].Add(val);
                }
            }
            else
            {
                lemmaMapping[key] = new List<LemmatizerItem>();
                lemmaMapping[key].Add(val);
            }
        }

        void AddAllInflectionsToDictionary(List<Inflection> inflections, String word, POS pos)
        {
            foreach (Inflection inflection in inflections)
            {
                AddToDictionary(inflection.InflectedWord, word, pos);
            }
        }

        void AddFormsToDictionary(List<String> forms, String word, POS pos)
        {
            foreach (String form in forms)
            {
                AddToDictionary(form, word, pos);
            }
        }

        void AddImperativeFormsToDictionary(List<String> forms, String lemma)
        {
            foreach (String imperativ in forms)
            {
                if (imperativ.EndsWith("!"))
                {
                    AddToDictionary(imperativ.Substring(0, imperativ.Length - 1), lemma, POS.Verb);
                }
                else
                {
                    AddToDictionary(imperativ, lemma, POS.Verb);
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
                Lemmas = x.Value,
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
            return this.lemmaMapping.ContainsKey(word.ToLower());
        }

        /// <summary>
        /// Return if the given word form is present in IWNLP
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool ContainsEntry(String word, POS pos)
        {
            return this.ContainsEntry(word, pos, false);
        }

        public bool ContainsEntry(String word, POS pos, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return this.lemmaMapping.ContainsKey(word.ToLower()) && this.lemmaMapping[word.ToLower()].Any(x => x.POS == pos);
            }
            else
            {
                return this.lemmaMapping.ContainsKey(word.ToLower()) && this.lemmaMapping[word.ToLower()].Any(x => x.POS == pos && x.Form == word);
            }
        }

        public bool ContainsEntry(String word, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return this.lemmaMapping.ContainsKey(word.ToLower()) && this.lemmaMapping[word.ToLower()].Any();
            }
            else
            {
                return this.lemmaMapping.ContainsKey(word.ToLower()) && this.lemmaMapping[word.ToLower()].Any(x => x.Form == word);
            }
        }

        /// <summary>
        /// Return if the given word form is present in IWNLP
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool ContainsEntry(String word, List<POS> pos)
        {
            return ContainsEntry(word, pos, false);
        }

        public bool ContainsEntry(String word, List<POS> pos, bool ignoreCase)
        {
            foreach (POS posItem in pos)
            {
                if (this.ContainsEntry(word, posItem, ignoreCase))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Return all lemmas for a given inflected form
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<String> GetLemmas(String word)
        {
            return this.GetLemmas(word, false);
        }

        public List<String> GetLemmas(String word, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return this.lemmaMapping[word.ToLower()].Select(x => x.Lemma).Distinct().ToList();
            }
            else
            {
                return this.lemmaMapping[word.ToLower()].Where(x => x.Form == word).Select(x => x.Lemma).Distinct().ToList();
            }

        }



        /// <summary>
        /// Return all lemmas for a given inflected form
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<String> GetLemmas(String word, POS pos, bool ignoreCase)
        {
            List<LemmatizerItem> items = this.lemmaMapping[word.ToLower()].Where(x => x.POS == pos).ToList(); // the key in the dictionary is case insensitive
            if (!ignoreCase)
            {
                items = items.Where(x => x.Form == word).ToList(); // case sensitive comparison
            }
            return items.Select(x => x.Lemma).Distinct().ToList();
        }

        public List<String> GetLemmas(String word, POS pos)
        {
            return this.GetLemmas(word, pos, false);
        }



        public List<String> GetLemmas(String word, List<POS> pos)
        {
            return this.GetLemmas(word, pos, false);
        }

        public List<String> GetLemmas(String word, List<POS> pos, bool ignoreCase)
        {
            List<String> lemmas = new List<string>();
            foreach (POS postItem in pos)
            {
                lemmas = lemmas.Union<String>(this.GetLemmas(word, postItem, ignoreCase)).ToList();
            }
            return lemmas.Distinct().ToList();
        }


    }
}
