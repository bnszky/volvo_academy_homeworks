using BookAnalyzer;
using BookAnalyzer.Models;
using System;
using System.IO;

class Program
{
    static async Task Main()
    {
        BookHandler.ClearResults();
        var books = await BookHandler.ReadBasicBookInfo();
        await BookHandler.RunForAllBooks(books);
        Console.ReadKey();
    }
}
