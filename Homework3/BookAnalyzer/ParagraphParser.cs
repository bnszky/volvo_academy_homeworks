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
            List<String> sentences, words;
            words = new List<String>();
            //Console.WriteLine(text.ToString());

            sentences = Regex.Split(text.ToString(), @"(?<=[\.!\?])\s+").ToList();

            foreach ( var sentence in sentences)
            {
                sentence.Trim();

                words = Regex.Split(sentence, @"\W+").ToList();
                words = words.Where(word => !string.IsNullOrEmpty(word)).ToList();

                //MatchCollection punctuation = Regex.Matches(sentence, @"\p{P}");
                //foreach ( var xd in punctuation) {
                //    Console.WriteLine(xd);
                //}
            }

            return new Tuple<List<String>, List<String>, List<String>>(sentences, words, words);
        }
    }
}
