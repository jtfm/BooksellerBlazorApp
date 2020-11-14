using Bookseller.Shared;
using Microsoft.EntityFrameworkCore;

namespace Bookseller.DAL
{
    public class BooksellerContext : DbContext
    {
        public BooksellerContext(DbContextOptions<BooksellerContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Books;Trusted_Connection=True;");
            }
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
