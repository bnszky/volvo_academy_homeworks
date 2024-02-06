using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookAnalyzer
{
    public static class ParagraphParser
    {
        public static Tuple<List<String>, List<String>, List<String>> Run(StringBuilder text)
        {
            List<String> sentences, words, punctuation;
            words = new List<String>();
            punctuation = new List<String>();

            MatchCollection wordMatches = Regex.Matches(text.ToString(), @"[\p{L}0-9']+");
            foreach (Match word in wordMatches) { words.Add(word.Value.ToLower()); }

            sentences = Regex.Split(text.ToString(), @"(?<=[\.!\?])\s+").ToList();

            sentences = sentences.Where(sentence => !string.IsNullOrEmpty(sentence) && Regex.IsMatch(sentence, @"\p{L}[?.!]$") && !Regex.IsMatch(sentence, @"[I|V|X|L|C|D|M]\.") && WordHandler.GetNumberOfWordsFromSentence(sentence) >= 3).ToList();

            foreach(var sentence in sentences)
            {
                punctuation.AddRange(Regex.Split(text.ToString(), @"[,;]").ToList());
            }
 
            return new Tuple<List<String>, List<String>, List<String>>(sentences, words, punctuation);
        }
    }
}
