using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Commands
{
    public interface ICreateBookCommand
    {
        Task Execute(Book Book);
    }
}
