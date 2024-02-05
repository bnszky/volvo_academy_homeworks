using System;
using System.Collections;
using System.Collections.Generic;
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
        public int Id { get; }
        private string source;
        public BookInfo info { get; set; }

        private String longestSentenceByNumberOfCharacters;
        private String shortestSentenceByNumberOfWords;
        private String longestWord;
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
                            ParagraphParser.Run(paragraph);
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
                                List<string> sentences = new List<string>();
                                List<string> words = new List<string>();
                                List<string> punctuation = new List<string>();
                                (sentences, words, punctuation) = ParagraphParser.Run(paragraph);
                                WordHandler.FindLongestSentenceByNumberOfCharacters(sentences, ref longestSentenceByNumberOfCharacters);
                                WordHandler.FindShortestSentenceByNumberOfWords(sentences, ref shortestSentenceByNumberOfWords);
                                WordHandler.FindLongestWord(words, ref longestWord);
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
            Console.WriteLine($"Book \"{info.Title}\" has been read");
        }
        public void WriteData(String destination)
        {
            var convertedTitle = WordHandler.GetValidFileName(info.Title);
            var path = $"{destination}\\{convertedTitle}.txt";

            var sortedWords = WordHandler.SortDictionaryInDescendingOrder(wordCounts);
            var sortedCharacters = WordHandler.SortDictionaryInDescendingOrder(characterCounts);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"The Longest Sentence by number of characters: {longestSentenceByNumberOfCharacters}");
                sw.WriteLine($"The Shortest Sentence by number of words: {shortestSentenceByNumberOfWords}");
                sw.WriteLine($"The Longest Word: {longestWord}");
                int count = 0;
                sw.WriteLine("Top 10 most occurance of characters: ");
                foreach(var pair in sortedCharacters)
                {
                    if (count >= 10) break;
                    count++;

                    sw.WriteLine($"{count}. {pair.Key} {pair.Value}");
                }
                sw.WriteLine("Most often words: ");
                foreach (var pair in sortedWords)
                {
                    sw.WriteLine(pair.Key + " " + pair.Value);
                }
            }
            Console.WriteLine($"Results of parsing book \"{info.Title}\" has been written to the file");
        }
    }
}
