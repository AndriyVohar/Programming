using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Models
{
    internal class BookRepository : IRepository<Book>
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetByTitle(string title)
        {
            return _context.Books.Where(b => b.Title.StartsWith(title)).ToList();
        }

        public List<Book> GetByTitleAndYearBetween(string title, int[] year)
        {
            return _context.Books
                .Where(b => b.Title.StartsWith(title) && b.Year >= year[0] && b.Year <= year[1])
                .ToList();
        }

        public List<Book> GetDifferentValues()
        {
            return _context.Books.Distinct().ToList();
        }

        public List<Book> GetMinValues()
        {
            return _context.Books.OrderBy(b => b.Id).Take(1).ToList();
        }

        public List<Book> GroupByTitle(string title)
        {
            return _context.Books
                .Where(b => b.Title.StartsWith(title))
                .GroupBy(b => b.Title)
                .Select(group => new Book
                {
                    Title = group.Key,
                    Author = "Author", // Placeholder value, update as needed
                    Year = 0 // Placeholder value, update as needed
                }).ToList();
        }

        public List<Book> GetOrderedByField(string field, bool ascending = true)
        {
            var query = _context.Books.OrderBy(b => EF.Property<object>(b, field));
            if (!ascending)
            {
                query = query.OrderByDescending(b => EF.Property<object>(b, field));
            }
            return query.ToList();
        }

        public bool Update(Book book)
        {
            var existingBook = _context.Books.Find(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
