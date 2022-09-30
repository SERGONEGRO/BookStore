using System;
using System.Threading.Tasks;
using BookStore.Domain.Commands;
using BookStore.EntityFramework.DTOs;

namespace BookStore.EntityFramework.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        private readonly BookStoreDbContextFactory _contextFactory;

        public DeleteBookCommand(BookStoreDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (BookStoreDbContext context = _contextFactory.Create())
            {
                BookDto BookDto = new BookDto()
                {
                    Id = id,
                };

                context.BookStore.Remove(BookDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
