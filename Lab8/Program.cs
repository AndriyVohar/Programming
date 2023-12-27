using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class Program
{
    static async Task Main(string[] args)
    {
        using (var context = new BookDbcontext())
        {
            var rep = new BookRepository(context);

            while (true)
            {
                Console.WriteLine("Таблиця бібліотека. Введіть яку операцію ви хочете робити з таблицею:");
                Console.WriteLine("a - вибрати всі книги;");
                Console.WriteLine("b - вибрати книгу за назвою;");
                Console.WriteLine("c - вибрати книгу за назвою та інтервалом років які були видані;");
                Console.WriteLine("d - знайти унікальні книги;");
                Console.WriteLine("e - знайти всі мінімальні поля;");
                Console.WriteLine("f - вибрати з угрупуванням за полем;");
                Console.WriteLine("g - запит із сортуванням по заданому полю в порядку зростання та спадання значень;");
                Console.WriteLine("h - редагування книги за ID");
                Console.WriteLine("exit - вихід з програми");
                Console.Write("Ваш вибір: ");
                string request = Console.ReadLine().ToLower();

                switch (request)
                {
                    case "a":
                        await DisplayBooksAsync(rep.GetAllAsync());
                        break;
                    case "b":
                        Console.Write("Введіть першу букву назви книги: ");
                        await DisplayBooksAsync(rep.GetByTitleAsync(Console.ReadLine()));
                        break;
                    case "c":
                        await DisplayBooksAsync(GetBooksByTitleAndYearAsync(rep));
                        break;
                    case "d":
                        await DisplayBooksAsync(rep.GetDifferentValuesAsync());
                        break;
                    case "e":
                        await DisplayBooksAsync(rep.GetMinValuesAsync());
                        break;
                    case "f":
                        Console.Write("Введіть першу букву назви книги: ");
                        await DisplayBooksAsync(rep.GroupByTitleAsync(Console.ReadLine()));
                        break;
                    case "g":
                        await DisplayBooksAsync(GetBooksOrderedByFieldAsync(rep));
                        break;
                    case "h":
                        await UpdateBookAsync(rep);
                        await DisplayBooksAsync(rep.GetAllAsync());
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Такого не існує");
                        break;
                }
            }
        }
    }

    static async Task DisplayBooksAsync(Task<List<Book>> booksTask)
    {
        List<Book> books = await booksTask;
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}");
        }
        Console.ReadLine();  // Pause for user input
    }

    static async Task<List<Book>> GetBooksByTitleAndYearAsync(BookRepository rep)
    {
        Console.Write("Введіть першу букву назви книги: ");
        string title = Console.ReadLine();
        int[] year = GetYearIntervalFromUser();
        return await rep.GetByTitleAndYearBetweenAsync(title, year);
    }

    static async Task<List<Book>> GetBooksOrderedByFieldAsync(BookRepository rep)
    {
        Console.Write("Введіть поле за яким групувати (title / author / year): ");
        string field = Console.ReadLine().ToLower();
        return await rep.GetOrderedByFieldAsync(field);
    }

    static async Task UpdateBookAsync(BookRepository rep)
    {
        Console.Write("Введіть Id: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Введіть назву");
            string title = Console.ReadLine();
            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();
            Console.Write("Введіть рік: ");
            int year = int.Parse(Console.ReadLine());
            Book book = new Book() { Id = id, Title = title, Author = author, Year = year };  // Update only the first year for simplicity
            await rep.UpdateAsync(book);
        }
        else
        {
            Console.WriteLine("Некоректний формат Id.");
        }
    }

    static int[] GetYearIntervalFromUser()
    {
        int[] year = new int[2];
        Console.Write("Введіть рік від якого хочете почати: ");
        year[0] = int.TryParse(Console.ReadLine(), out int startYear) ? startYear : 0;
        Console.Write("Введіть рік яким хочете закінчити: ");
        year[1] = int.TryParse(Console.ReadLine(), out int endYear) ? endYear : DateTime.Now.Year;
        return year;
    }
}
