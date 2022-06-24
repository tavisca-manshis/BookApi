using Microsoft.EntityFrameworkCore;
namespace BookApi.Contracts
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