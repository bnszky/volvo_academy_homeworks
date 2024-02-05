using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookAnalyzer
{
    public static class WordHandler
    {
        public static void FindLongestSentenceByNumberOfCharacters(List<String> sentences, ref String currentLongestSentence)
        {
            foreach (String sentence in sentences)
            {
                if (currentLongestSentence == null || sentence.Length > currentLongestSentence.Length) {
                    currentLongestSentence = sentence;
                }
            }
        }

        public static void FindShortestSentenceByNumberOfWords(List<String> sentences, ref String currentShortestSentence)
        {
            int currentShortestSentenceNumberOfWords = -1;
            if (currentShortestSentence != null)
            {
                var _words = Regex.Split(currentShortestSentence, @"\W+").ToList();
                _words = _words.Where(word => !string.IsNullOrEmpty(word)).ToList();
                currentShortestSentenceNumberOfWords = _words.Count(); 
            }
            foreach (String sentence in sentences)
            {
                //Console.WriteLine(sentence);
                //Console.WriteLine(currentShortestSentence==null ? "NULL" : currentShortestSentence);
                var words = Regex.Split(sentence, @"\W+").ToList();
                words = words.Where(word => !string.IsNullOrEmpty(word)).ToList();
                int numberOfWords = words.Count();

                //foreach (var word in words)
                //{
                //    Console.WriteLine(word);
                //}
                //Console.WriteLine(numberOfWords);
                //Console.ReadKey();
                if((currentShortestSentenceNumberOfWords == -1 || numberOfWords < currentShortestSentenceNumberOfWords) && numberOfWords > 0)
                {
                    currentShortestSentenceNumberOfWords = numberOfWords;
                    currentShortestSentence = sentence;
                }
            }
        }

        public static void FindLongestWord(List<String> words, ref String longestWord)
        {
            foreach(String word in words)
            {
                if(longestWord == null || word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
        }

        public static void AddCharacters(List<String> words, Dictionary<Char, int> characters)
        {
            foreach (String word in words)
            {
                foreach (char c in word)
                {
                    if (char.IsLetter(c))
                    {
                        char lowerC = char.ToLower(c);
                        if (characters.ContainsKey(lowerC))
                        {
                            characters[lowerC]++;
                        }
                        else
                        {
                            characters[lowerC] = 1;
                        }
                    }
                }
            }
        }

        public static void AddWords(List<String> words, Dictionary<String, int> wordsWithOccurences)
        {
            foreach (string word in words)
            {
                string _word = word.ToLower();
                if (wordsWithOccurences.ContainsKey(_word))
                {
                    wordsWithOccurences[_word]++;
                }
                else
                {
                    wordsWithOccurences[_word] = 1;
                }
            }
        }

        public static string ToPascalCase(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string pascalCase = textInfo.ToTitleCase(input);
            pascalCase = Regex.Replace(pascalCase, @"[\s\t]", string.Empty);
            return pascalCase;
        }
        public static string GetValidFileName(string title)
        {
            string pascalCaseTitle = ToPascalCase(title);
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string validFileName = Regex.Replace(pascalCaseTitle, "[" + Regex.Escape(invalidChars) + "]", "");
            return validFileName;
        }

        public static Dictionary<TKey, int> SortDictionaryInDescendingOrder<TKey>(Dictionary<TKey, int> dict)
        {
            return dict.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

    }
}
