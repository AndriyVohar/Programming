using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace Lab_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Завдання
            ////1.Використовуючи інструмент SQL Server Object Explorer у Visual Studio створити БД з іменем
            //назва_групи_прізвище_студента.
            ////2.Створити таблицю у базі даних у відповідності до індивідуального варіанту.Наповнити таблицю даними
            //(не менше 20 записів).
            //3.Cтворити наступні види SQL - запитів:
            //a) простий запит на вибірку;
            //b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
            //c) запит зі складним критерієм;
            //d) запит з унікальними значеннями;
            //e) запит з використанням обчислювального поля;
            //f) запит з групуванням по заданому полю, використовуючи умову групування;
            //g) запит із сортування по заданому полю в порядку зростання та спадання значень;
            //h) запит з використанням дій по модифікації записів.

            //Варіант 4.Створити таблицю бази даних про книги, які були куплені бібліотекою: назва, автор, рік видання,
            //адреса автора, адреса видавництва, ціна, книготорговельна фірма.


            //Лабораторна 6
            //Використовуючи SqlCommand підготувати програмну оболонку для виконання завдань лабораторної роботи 5.
            //Забезпечити користувачу можливість ввести значення параметрів запиту.


            const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Lenovo x270\Documents\SDHC\patterns\Lab6\Lab-5\sa2_vohar.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var rep = new BookRepository(connection);

                while (true)
                {
                    Start: Console.WriteLine("Таблиця бібліотека. Введіть яку операцію ви хочете робити з таблицею:");
                    Console.WriteLine("а - вибрати всі книги;");
                    Console.WriteLine("b - вибрати книгу за назвою;");
                    Console.WriteLine("с - вибрати книгу за назвою та інтервалом років які були видані;");
                    Console.WriteLine("d - знайти унікальні книги;");
                    Console.WriteLine("e - знайти всі мінімальні поля;");
                    Console.WriteLine("f - вибрати з угрупуванням за полем;");
                    Console.WriteLine("g - запит із сортування по заданому полю в порядку зростання та спадання значень;");
                    Console.WriteLine("h - редагування книги за ID");
                    Console.WriteLine("exit - редагування книги за ID");
                    Console.WriteLine(":");
                    string request = Console.ReadLine();
                    List<Book> books = rep.GetAll();
                    if (request.ToLower() == "a")
                    {
                        books = rep.GetAll();
                    } 
                    else if (request.ToLower() == "b")
                    {
                        Console.Write("Введіть першу букву назви книги: ");
                        books = rep.GetByTitle(Console.ReadLine());
                    }
                    else if (request.ToLower() == "c")
                    {
                        Console.Write("Введіть першу букву назви книги: ");
                        string title = Console.ReadLine();
                        int[] year = new int[2];
                        Console.Write("Введіть рік від якого хочете почати: ");
                        year[0] = int.Parse(Console.ReadLine());
                        Console.Write("Введіть рік яким хочете закінчити: ");
                        year[1] = int.Parse(Console.ReadLine());
                        books = rep.GetByTitleAndYearBetween(title, year);
                    }
                    else if (request.ToLower() == "d")
                    {
                        books = rep.getDifferentValues();
                    }
                    else if (request.ToLower() == "e")
                    {
                        books = rep.getMinValues();
                    }
                    else if (request.ToLower() == "f")
                    {
                        Console.Write("Введіть першу букву назви книги: ");
                        string title = Console.ReadLine();
                        books = rep.groupByTitle(title);
                    }
                    else if (request.ToLower() == "g") 
                    {
                        Console.Write("Введіть поле за яким групувати (title / author / year): ");
                        string field = Console.ReadLine().ToLower();
                        books = rep.getOrderedByField(field);
                    }
                    else if (request.ToLower() == "h")
                    {
                        Console.Write("Введіть Id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Введіть title: ");
                        string title = Console.ReadLine();
                        Console.Write("Введіть autor: ");
                        string author = Console.ReadLine();
                        Console.Write("Введіть year: ");
                        int year = int.Parse(Console.ReadLine());
                        Book book = new Book() { Id = id, title = title, author = author, year = year};
                        rep.Update(book);
                        books = rep.GetAll();
                    }
                    else if (request.ToLower() == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Такого не існує");
                        goto End;
                    }

                    foreach (var book in books)
                    {
                        Console.WriteLine($"Title: {book.title}, Author: {book.author}, Year: {book.year}");
                    }
                    End: Console.ReadLine();
                    goto Start;
                }
            }

        }
    }
}
