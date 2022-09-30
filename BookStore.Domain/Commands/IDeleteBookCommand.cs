using System;
using System.Threading.Tasks;

namespace BookStore.Domain.Commands
{
    public interface IDeleteBookCommand
    {
        Task Execute(Guid id);
    }
}
