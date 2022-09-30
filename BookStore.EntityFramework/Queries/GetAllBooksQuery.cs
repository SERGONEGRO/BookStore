using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using BookStore.Domain.Queries;
using BookStore.EntityFramework.DTOs;

namespace BookStore.EntityFramework.Queries
{
    public class GetAllBookStoreQuery : IGetAllBookStoreQuery
    {
        private readonly BookStoreDbContextFactory _contextFactory;

        public GetAllBookStoreQuery(BookStoreDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Book>> Execute()
        {
            using (BookStoreDbContext context = _contextFactory.Create())
            {
                IEnumerable<BookDto> BookDtos = await context.BookStore.ToListAsync();

                return BookDtos.Select(b => new Book(b.Id, b.Name, b.Author, b.Year, b.Isbn, b.Desc));
            }
        }
    }
}
