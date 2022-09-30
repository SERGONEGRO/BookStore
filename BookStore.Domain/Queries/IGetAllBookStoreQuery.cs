using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Queries
{
    public interface IGetAllBookStoreQuery
    {
        Task<IEnumerable<Book>> Execute();
    }
}
