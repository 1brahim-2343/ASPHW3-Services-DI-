using ASPHW3_Services__DI_.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPHW3_Services__DI_.Data
{
    public class BookContext:DbContext
    {
        public BookContext(DbContextOptions<BookContext> options):base(options)
        {
          
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(10, 5);
        }
    }
}
