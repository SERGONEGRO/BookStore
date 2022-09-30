using Microsoft.EntityFrameworkCore;

namespace BookStore.EntityFramework
{
    public class BookStoreDbContextFactory
    {
        private readonly DbContextOptions _options;

        public BookStoreDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public BookStoreDbContext Create()
        {
            return new BookStoreDbContext(_options);
        }
    }
}
