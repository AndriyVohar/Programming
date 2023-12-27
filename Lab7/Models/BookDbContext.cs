using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.SqlServer;

namespace Lab7.Models
{
    internal class BookDbcontext: DbContext
    {
        public BookDbcontext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Book> Library { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = ""C:\Users\Lenovo x270\Documents\SDHC\Програмування (C#)\Lab7\Lab7\sa2_vohar.mdf""; Integrated Security = True");
        }

    }
}
