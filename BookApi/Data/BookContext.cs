using BookApi.Contracts;
using Microsoft.EntityFrameworkCore;
namespace BookApi.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
    }
}