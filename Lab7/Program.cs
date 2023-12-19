using Lab7.Models;

internal class Program
{
    static void Main(string[] args)
    {
        using (var context = new BookDbContext())
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
                        DisplayBooks(rep.GetAll());
                        break;
                    case "b":
                        Console.Write("Введіть першу букву назви книги: ");
                        DisplayBooks(rep.GetByTitle(Console.ReadLine()));
                        break;
                    case "c":
                        DisplayBooks(GetBooksByTitleAndYear(rep));
                        break;
                    case "d":
                        DisplayBooks(rep.GetDifferentValues());
                        break;
                    case "e":
                        DisplayBooks(rep.GetMinValues());
                        break;
                    case "f":
                        Console.Write("Введіть першу букву назви книги: ");
                        DisplayBooks(rep.GroupByTitle(Console.ReadLine()));
                        break;
                    case "g":
                        DisplayBooks(GetBooksOrderedByField(rep));
                        break;
                    case "h":
                        UpdateBook(rep);
                        DisplayBooks(rep.GetAll());
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

    static void DisplayBooks(List<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.Year}");
        }
        Console.ReadLine();  // Pause for user input
    }

    static List<Book> GetBooksByTitleAndYear(BookRepository rep)
    {
        Console.Write("Введіть першу букву назви книги: ");
        string title = Console.ReadLine();
        int[] year = GetYearIntervalFromUser();
        return rep.GetByTitleAndYearBetween(title, year);
    }

    static List<Book> GetBooksOrderedByField(BookRepository rep)
    {
        Console.Write("Введіть поле за яким групувати (title / author / year): ");
        string field = Console.ReadLine().ToLower();
        return rep.GetOrderedByField(field);
    }

    static void UpdateBook(BookRepository rep)
    {
        Console.Write("Введіть Id: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Введіть title: ");
            string title = Console.ReadLine();
            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();
            int[] year = GetYearIntervalFromUser();
            Book book = new Book() { Id = id, Title = title, Author = author, Year = year[0] };  // Update only the first year for simplicity
            rep.Update(book);
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
