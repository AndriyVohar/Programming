using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.SqlServer;

namespace Lab7.Models
{
    internal class BookRepository : IRepository<Book>
    {
        protected BookDbcontext _context;

        public BookRepository(BookDbcontext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Library.ToList();
        }

        public List<Book> GetByTitle(string title)
        {
            return _context.Library.Where(b => b.Title.StartsWith(title)).ToList();
        }

        public List<Book> GetByTitleAndYearBetween(string title, int[] year)
        {
            return _context.Library
                .Where(b => b.Title.StartsWith(title) && b.Year >= year[0] && b.Year <= year[1])
                .ToList();
        }

        public List<Book> GetDifferentValues()
        {
            return _context.Library.Distinct().ToList();
        }

        public List<Book> GetMinValues()
        {
            return _context.Library.OrderBy(b => b.Id).Take(1).ToList();
        }

        public List<Book> GroupByTitle(string title)
        {
            return _context.Library
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
            var query = _context.Library.OrderBy(b => EF.Property<object>(b, field));
            if (!ascending)
            {
                query = query.OrderByDescending(b => EF.Property<object>(b, field));
            }
            return query.ToList();
        }

        public bool Update(Book book)
        {
            var existingBook = _context.Library.Find(book.Id);
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
