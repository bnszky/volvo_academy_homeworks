using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAnalyzer.Models;

namespace BookAnalyzer
{
    public static class BookHandler
    {
        private const String DATA_PATH = "C:\\Users\\micha\\source\\repos\\volvo_academy_homeworks\\Homework3\\BookAnalyzer\\Data";
        private const String RESULT_PATH = DATA_PATH + "\\Results";
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
            }
            else
            {
                Console.WriteLine("Directory {0} already exists.", path);
            }
        }

        // Read title, author and language and create instance of object
        public static async Task<List<Book>> ReadBasicBookInfo()
        {
            List<String> files = Directory.GetFiles(DATA_PATH + "\\100 books").ToList();

            List<Book> books = new List<Book>();
            List<Task> tasks = new List<Task>();
            files.ForEach(file =>
            {
                tasks.Add(Task.Run(() =>
                {
                    books.Add(new Book(file));
                }));
            });

            await Task.WhenAll(tasks);
            return books;
        }

        // Run algorithm for all books asynchronously
        public static async Task RunForAllBooks(List<Book> books)
        {
            List<Task> tasks = new List<Task>();
            CreateDirectory(RESULT_PATH);
            foreach (Book book in books)
            {
                tasks.Add(Task.Run(() =>
                {
                    book.Read();
                    book.WriteData(RESULT_PATH);
                }));
            }
        }

        // Run algorithm for only one book
        public static void RunForSpecificBook(List<Book> books, int id)
        {
            var book = books.Where(book => book.Id == id).FirstOrDefault();
            CreateDirectory(RESULT_PATH);
            if (book == null)
            {
                Console.WriteLine("Book with this id doesn't exist!");
                return;
            }

            book.Read();
            book.WriteData(RESULT_PATH);
        }

        // Clear and remove Result directory
        public static void ClearResults()
        {
            if (Directory.Exists(RESULT_PATH)) { Directory.Delete(RESULT_PATH, true); }
        }
    }
}
