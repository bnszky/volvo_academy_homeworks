using BookAnalyzer.Comparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalyzer
{
    public static class AllBookStatsHandler
    {
        public static SortedSet<String> longestSentencesByNumberOfCharacters;
        public static SortedSet<String> shortestSentencesByNumberOfWords;
        public static SortedSet<String> longestWords;
        public static Dictionary<String, int> wordCounts;
        public static Dictionary<char, int> characterCounts;
        
        public static void Init()
        {
            longestSentencesByNumberOfCharacters = new SortedSet<String>(new StringLengthComparer());
            shortestSentencesByNumberOfWords = new SortedSet<String>(new SentenceByNumberOfWordsComparer());
            longestWords = new SortedSet<String>(new StringLengthComparer());
            wordCounts = new Dictionary<string, int>();
            characterCounts = new Dictionary<char, int>();
        }

        public static void WriteDataToFile(String path)
        {
            var sortedWords = WordHandler.SortDictionaryInDescendingOrder(wordCounts);
            var sortedCharacters = WordHandler.SortDictionaryInDescendingOrder(characterCounts);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("Stats for all files");
                int index = 0;

                sw.WriteLine($"\nTop 10 Longest Sentences by number of characters: ");

                foreach (var sentence in longestSentencesByNumberOfCharacters.Reverse())
                {
                    sw.WriteLine($"{++index}. {sentence}");
                }

                index = 0;

                sw.WriteLine($"\nTop 10 Shortest Sentences by number of words: ");

                foreach (var sentence in shortestSentencesByNumberOfWords)
                {
                    sw.WriteLine($"{++index}. {sentence}");
                }

                index = 0;

                sw.WriteLine($"\nThe 10 Longest Words: ");

                foreach (var word in longestWords.Reverse())
                {
                    sw.WriteLine($"{++index}. {word}");
                }

                int count = 0;
                sw.WriteLine("\nTop 10 most characters: ");
                foreach (var pair in sortedCharacters)
                {
                    if (count >= 10) break;
                    count++;

                    sw.WriteLine($"{count}. {pair.Key} {pair.Value}");
                }
                sw.WriteLine("\nMost often words: ");
                foreach (var pair in sortedWords)
                {
                    sw.WriteLine(pair.Key + " " + pair.Value);
                }
            }

            Console.WriteLine($"Stats have been written to the file {path}");
        }
    }
}
