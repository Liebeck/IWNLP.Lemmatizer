# IWNLP.Lemmatizer
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/Liebeck/IWNLP.Lemmatizer/LICENSE.md)  
IWNLP is a dictionary-based lemmatizer for the German language. It is based on the German edition of Wiktionary. IWNLP consists of two parts:
* [IWNLP](https://github.com/Liebeck/IWNLP): A parser for the German edition of Wiktionary
* IWNLP.Lemmatizer: A German lemmatizer that uses the output from IWNLP to produce a mapping from an inflected form to a lemma.

More details can be found at www.iwnlp.com

# How to run IWNLP.Lemmatizer
* Make sure that you followed the steps from [IWNLP](https://github.com/Liebeck/IWNLP) regarding the creation of a parsed XML Wiktionary file.
* Clone IWNLP.Lemmatizer and build it
* Start IWNLP.Lemmatizer.exe with two parameters: Path to parsed Wiktionary dump, path to the export file. For instance
``` bash
IWNLP.Parser.exe "c:\\parsedIWNLP_latest.xml" "c:\\IWNLP.Lemmatizer_latest.xml"
```

# Citation
Please include the following BibTeX if you use IWNLP in your work:
``` bash
@InProceedings{liebeck-conrad:2015:ACL-IJCNLP,
  author    = {Liebeck, Matthias  and  Conrad, Stefan},
  title     = {{IWNLP: Inverse Wiktionary for Natural Language Processing}},
  booktitle = {Proceedings of the 53rd Annual Meeting of the Association for Computational Linguistics and the 7th International Joint Conference on Natural Language Processing (Volume 2: Short Papers)},
  year      = {2015},
  publisher = {Association for Computational Linguistics},
  pages     = {414--418},
  url       = {http://www.aclweb.org/anthology/P15-2068}
}
```