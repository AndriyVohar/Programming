using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Lab_5
{
    internal class BookRepository:IRepository<Book>
    {
        protected SqlConnection _connection;
        public BookRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        //a) простий запит на вибірку;
        public List<Book> GetAll()
        {
            var books = new List<Book>();
            string query = "SELECT * FROM library";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    }
                }
            }
            return books;
        }

        //b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
        public List<Book> GetByTitle(string title)
        {
            var books = new List<Book>();
            string querty = $"SELECT * FROM library WHERE title LIKE '{title}%';";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //c) запит зі складним критерієм;
        public List<Book> GetByTitleAndYearBetween(string title, int[]year)
        {
            var books = new List<Book>();
            string querty = $"SELECT * FROM library WHERE title LIKE '{title}%' AND year between {year[0]} AND {year[1]};";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //d) запит з унікальними значеннями;
        public List<Book> getDifferentValues()
        {
            var books = new List<Book>();
            string querty = $"select distinct * from library;";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //e) запит з використанням обчислювального поля;
        public List<Book> getMinValues()
        {
            var books = new List<Book>();
            string querty = $"select min(Id) as Id, min(author) as author, min(title) as title, min(year) as year from library;";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //f) запит з групуванням по заданому полю, використовуючи умову групування;
        public List<Book> groupByTitle(string title)
        {
            var books = new List<Book>();
            string querty = $"select title from library where title like '{title}%' GROUP BY title;";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = 0,
                            title = Convert.ToString(reader["title"]),
                            author = "author",
                            year = 0
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //g) запит із сортування по заданому полю в порядку зростання та спадання значень;
        public List<Book> getOrderedByField(string field)
        {
            string querty = $"select * from library order by {field} asc;";
            var books = new List<Book>();
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            querty = $"select * from library order by {field} desc;";
            using (SqlCommand command = new SqlCommand(querty, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            title = Convert.ToString(reader["title"]),
                            author = Convert.ToString(reader["author"]),
                            year = Convert.ToInt32(reader["year"]),
                        };
                        books.Add(book);
                    };
                }
            }
            return books;
        }

        //h) запит з використанням дій по модифікації записів.
        public bool Update(Book book)
        {
            using (SqlCommand command = new SqlCommand(
               String.Format("UPDATE library SET title='{0}', author={1}, year={2} WHERE Id={3};", book.title, book.author, book.year, book.Id),
               _connection))
            {
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
