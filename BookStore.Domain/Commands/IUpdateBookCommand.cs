using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Commands
{
    public interface IUpdateBookCommand
    {
        Task Execute(Book Book);
    }
}
