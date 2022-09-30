using Microsoft.EntityFrameworkCore;
using BookStore.EntityFramework.DTOs;

namespace BookStore.EntityFramework
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<BookDto> BookStore { get; set; }
    }
}
