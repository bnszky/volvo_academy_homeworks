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
        public static void FindLongestSentenceByNumberOfCharacters(List<String> sentences, SortedSet<String> currentLongestSentences)
        {
            foreach (String sentence in sentences)
            {
                if(currentLongestSentences.Count == 10 && sentence.Length > currentLongestSentences.Min.Length)
                {
                    currentLongestSentences.Remove(currentLongestSentences.Min);
                    currentLongestSentences.Add(sentence);
                }
                else if(currentLongestSentences.Count < 10)
                {
                    currentLongestSentences.Add(sentence);
                }
            }
        }

        public static int GetNumberOfWordsFromSentence(String sentence)
        {
            if(sentence == null || sentence.Length == 0) return 0;

            MatchCollection wordMatches = Regex.Matches(sentence, @"[\p{L}0-9']+");
            return wordMatches.Count;
        }

        public static void FindShortestSentenceByNumberOfWords(List<String> sentences, SortedSet<String> currentShortestSentences)
        {
            foreach(String sentence in sentences)
            {
                if(currentShortestSentences.Count == 10 && GetNumberOfWordsFromSentence(currentShortestSentences.Max) > GetNumberOfWordsFromSentence(sentence))
                {
                    currentShortestSentences.Remove(currentShortestSentences.Max);
                    currentShortestSentences.Add(sentence);
                }
                else if(currentShortestSentences.Count < 10)
                {
                    currentShortestSentences.Add(sentence);
                }
            }
        }

        public static void FindLongestWord(List<String> words, SortedSet<String> longestWords)
        {
            foreach (String word in words)
            {
                if (longestWords.Count == 10 && word.Length > longestWords.Min.Length)
                {
                    longestWords.Remove(longestWords.Min);
                    longestWords.Add(word);
                }
                else if(longestWords.Count < 10)
                {
                    longestWords.Add(word);
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

        public static void AddToDictionary<TKey>(Dictionary<TKey, int> dict1, Dictionary<TKey, int> dict2){
            foreach(var pair in dict2)
            {
                if (dict1.ContainsKey(pair.Key))
                {
                    dict1[pair.Key] += pair.Value;
                }
                else
                {
                    dict1.Add(pair.Key, pair.Value);
                }
            }
        }

    }
}
