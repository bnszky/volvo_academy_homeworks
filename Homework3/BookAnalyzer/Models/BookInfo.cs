using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalyzer.Models
{
    public record BookInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }

        public BookInfo(string title, string author, string language)
        {
            Title = title;
            Author = author;
            Language = language;
        }
    }
}
