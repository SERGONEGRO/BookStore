using System.Threading.Tasks;
using BookStore.Domain.Commands;
using BookStore.Domain.Models;
using BookStore.EntityFramework.DTOs;

namespace BookStore.EntityFramework.Commands
{
    public class UpdateBookCommand : IUpdateBookCommand
    {
        private readonly BookStoreDbContextFactory _contextFactory;

        public UpdateBookCommand(BookStoreDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Book Book)
        {
            using (BookStoreDbContext context = _contextFactory.Create())
            {
                BookDto BookDto = new BookDto()
                {
                    Id = Book.Id,
                    Name = Book.Name,
                    Author = Book.Author,
                    Year = Book.Year,
                    Isbn = Book.Isbn,
                    Image = Book.Image,
                    Desc = Book.Desc
                };

                context.BookStore.Update(BookDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
