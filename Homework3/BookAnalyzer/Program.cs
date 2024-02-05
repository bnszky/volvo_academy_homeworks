using BookAnalyzer.Models;
using System;
using System.IO;

class Program
{
    public const String DATA_PATH = "C:\\Users\\micha\\source\\repos\\volvo_academy_homeworks\\Homework3\\BookAnalyzer\\Data";

    static void CreateDirectory(string path)
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

    static void Main()
    {
        try
        {
            List<String> files = Directory.GetFiles(DATA_PATH + "\\100 books").ToList();
            var books = files.Select(path => new Book(path)).ToList();
            CreateDirectory(DATA_PATH + "\\Results");

            foreach (var book in books)
            {
                book.Read();
                book.WriteData(DATA_PATH + "\\Results");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadKey();
    }
}
