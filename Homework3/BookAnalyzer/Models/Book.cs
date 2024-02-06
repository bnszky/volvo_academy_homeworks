using BookAnalyzer.Comparers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BookAnalyzer.Models
{
    public class Book
    {
        private static object lockObject = new object();
        public int Id { get; }
        private string source;
        public BookInfo info { get; set; }

        private SortedSet<String> longestSentencesByNumberOfCharacters;
        private SortedSet<String> shortestSentencesByNumberOfWords;
        private SortedSet<String> longestWords;
        private Dictionary<String, int> wordCounts;
        private Dictionary<char, int> characterCounts;

        private BookInfo GetBookInfo()
        {
            try
            {
                string title, author, language;
                title = author = language = "";
                using (StreamReader sr = new StreamReader(source))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null && (title == "" || author == "" || language == ""))
                    {
                        if (line.StartsWith("Title: ")) { title = line.Substring("Title: ".Length); }
                        if (line.StartsWith("Author: ")) { author = line.Substring("Author: ".Length); }
                        if (line.StartsWith("Language: ")) { language = line.Substring("Language: ".Length); }
                    }

                    return new BookInfo(title, author, language);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public Book(string _source) { 
            wordCounts = new Dictionary<string, int> ();
            characterCounts = new Dictionary<char, int> ();
            longestSentencesByNumberOfCharacters = new SortedSet<string> (new StringLengthComparer());
            longestWords = new SortedSet<string> (new StringLengthComparer());
            shortestSentencesByNumberOfWords = new SortedSet<string> (new SentenceByNumberOfWordsComparer());

            source = _source;

            var pattern = @"pg(\d+)\.txt";
            
            var match = Regex.Match(source, pattern);
            if (match == null || match.Success != true) { throw new Exception("Invalid file name of the book"); }

            Id = int.Parse(match.Groups[1].Value);

            info = GetBookInfo();
            if(info == null) { throw new Exception("Couldn't read book info"); }
            Console.WriteLine($"Book {info.Title} #{Id} has been initialized");
        }
        public void Read() {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            List<string> sentences = new List<string>();
            List<string> words = new List<string>();
            List<string> punctuation = new List<string>();

            using (StreamReader sr = new StreamReader(source))
            {
                string line;
                bool isContentLine = false;
                StringBuilder paragraph = new StringBuilder();
                while ((line = sr.ReadLine()) != null) {
                    line = line.TrimStart();
                    if (line.StartsWith("*** START"))
                    {
                        isContentLine = true;
                        continue;
                    }
                    if (line.StartsWith("*** END"))
                    {
                        isContentLine = false;

                        if (paragraph.Length > 0)
                        {
                            (sentences, words, punctuation) = ParagraphParser.Run(paragraph);
                            WordHandler.FindLongestSentenceByNumberOfCharacters(sentences, longestSentencesByNumberOfCharacters);
                            WordHandler.FindShortestSentenceByNumberOfWords(sentences, shortestSentencesByNumberOfWords);
                            WordHandler.FindLongestWord(words, longestWords);
                            WordHandler.AddWords(words, wordCounts);
                            WordHandler.AddCharacters(words, characterCounts);
                            paragraph.Clear();
                        }

                        continue;
                    }

                    if (isContentLine)
                    {
                        if (line.Equals(""))
                        {
                            if (paragraph.Length > 0)
                            {
                                (sentences, words, punctuation) = ParagraphParser.Run(paragraph);
                                WordHandler.FindLongestSentenceByNumberOfCharacters(sentences, longestSentencesByNumberOfCharacters);
                                WordHandler.FindShortestSentenceByNumberOfWords(sentences, shortestSentencesByNumberOfWords);
                                WordHandler.FindLongestWord(words, longestWords);
                                WordHandler.AddWords(words, wordCounts);
                                WordHandler.AddCharacters(words, characterCounts);
                                paragraph.Clear();
                            }
                        }
                        else
                        {
                            paragraph.Append(line + "\n");
                        }
                    }
                }
            }

            lock (lockObject)
            {
                WordHandler.FindLongestSentenceByNumberOfCharacters(longestSentencesByNumberOfCharacters.ToList(), AllBookStatsHandler.longestSentencesByNumberOfCharacters);
                WordHandler.FindShortestSentenceByNumberOfWords(shortestSentencesByNumberOfWords.ToList(), AllBookStatsHandler.shortestSentencesByNumberOfWords);
                WordHandler.FindLongestWord(longestWords.ToList(), AllBookStatsHandler.longestWords);
                WordHandler.AddToDictionary(AllBookStatsHandler.characterCounts, characterCounts);
                WordHandler.AddToDictionary(AllBookStatsHandler.wordCounts, wordCounts);
            }

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            double seconds = ts.TotalSeconds;

            Console.WriteLine($"Book \"{info.Title}\" has been read in {seconds:F4} seconds");
        }
        public void WriteData(String destination)
        {
            var convertedTitle = WordHandler.GetValidFileName(info.Title);
            var path = $"{destination}\\{convertedTitle}.txt";

            var sortedWords = WordHandler.SortDictionaryInDescendingOrder(wordCounts);
            var sortedCharacters = WordHandler.SortDictionaryInDescendingOrder(characterCounts);

            using (StreamWriter sw = File.AppendText(path))
            {
                int index = 0;

                sw.WriteLine($"\nTop 10 Longest Sentences by number of characters: ");
                
                foreach (var sentence in longestSentencesByNumberOfCharacters.Reverse()) {
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
                foreach(var pair in sortedCharacters)
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
            Console.WriteLine($"Results of parsing book \"{info.Title}\" has been written to the file");
        }
    }
}
