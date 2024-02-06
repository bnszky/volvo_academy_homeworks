using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookAnalyzer.Comparers
{
    internal class SentenceByNumberOfWordsComparer : IComparer<string>
    {
        private int GetNumberOfWordsFromSentence(String sentence)
        {
            if (sentence == null || sentence.Length == 0) return 0;

            MatchCollection wordMatches = Regex.Matches(sentence, @"[\p{L}0-9']+");
            return wordMatches.Count;
        }
        public int Compare(string? x, string? y)
        {
            int lengthCompare = GetNumberOfWordsFromSentence((String)x).CompareTo(GetNumberOfWordsFromSentence((String)y));
            return lengthCompare == 0 ? x.CompareTo(y) : lengthCompare;
        }
    }
}
