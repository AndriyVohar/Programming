using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab7.Models
{
    internal class BookRepository
    {
        protected BookDbcontext _context;

        public BookRepository(BookDbcontext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Library.ToListAsync();
        }

        public async Task<List<Book>> GetByTitleAsync(string title)
        {
            return await _context.Library
                .Where(b => b.Title.StartsWith(title))
                .ToListAsync();
        }

        public async Task<List<Book>> GetByTitleAndYearBetweenAsync(string title, int[] year)
        {
            return await _context.Library
                .Where(b => b.Title.StartsWith(title) && b.Year >= year[0] && b.Year <= year[1])
                .ToListAsync();
        }

        public async Task<List<Book>> GetDifferentValuesAsync()
        {
            return await _context.Library.Distinct().ToListAsync();
        }

        public async Task<List<Book>> GetMinValuesAsync()
        {
            return await _context.Library.OrderBy(b => b.Id).Take(1).ToListAsync();
        }

        public async Task<List<Book>> GroupByTitleAsync(string title)
        {
            return await _context.Library
                .Where(b => b.Title.StartsWith(title))
                .GroupBy(b => b.Title)
                .Select(group => new Book
                {
                    Title = group.Key,
                    Author = "Author", // Placeholder value, update as needed
                    Year = 0 // Placeholder value, update as needed
                }).ToListAsync();
        }

        public async Task<List<Book>> GetOrderedByFieldAsync(string field, bool ascending = true)
        {
            var query = _context.Library.OrderBy(b => EF.Property<object>(b, field));
            if (!ascending)
            {
                query = query.OrderByDescending(b => EF.Property<object>(b, field));
            }
            return await query.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existingBook = await _context.Library.FindAsync(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
