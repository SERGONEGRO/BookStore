using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Commands;
using BookStore.Domain.Models;
using BookStore.Domain.Queries;

namespace BookStore.WPF.Stores
{
    public class BookStorage
    {
        private readonly IGetAllBookStoreQuery _getAllBookStoreQuery;
        private readonly ICreateBookCommand _createBookCommand;
        private readonly IUpdateBookCommand _updateBookCommand;
        private readonly IDeleteBookCommand _deleteBookCommand;

        private readonly List<Book> _BookStore;
        public IEnumerable<Book> BookStore => _BookStore;

        public event Action BookStoreLoaded;
        public event Action<Book> BookAdded;
        public event Action<Book> BookUpdated;
        public event Action<Guid> BookDeleted;

        public BookStorage(IGetAllBookStoreQuery getAllBookStoreQuery, 
            ICreateBookCommand createBookCommand, 
            IUpdateBookCommand updateBookCommand,
            IDeleteBookCommand deleteBookCommand)
        {
            _getAllBookStoreQuery = getAllBookStoreQuery;
            _createBookCommand = createBookCommand;
            _updateBookCommand = updateBookCommand;
            _deleteBookCommand = deleteBookCommand;

            _BookStore = new List<Book>();
        }

        public async Task Load()
        {
            IEnumerable<Book> BookStore = await _getAllBookStoreQuery.Execute();

            _BookStore.Clear();
            _BookStore.AddRange(BookStore);

            BookStoreLoaded?.Invoke();
        }

        public async Task Add(Book Book)
        {
            await _createBookCommand.Execute(Book);

            _BookStore.Add(Book);

            BookAdded?.Invoke(Book);
        }

        public async Task Update(Book Book)
        {
            await _updateBookCommand.Execute(Book);

            int currentIndex = _BookStore.FindIndex(y => y.Id == Book.Id);

            if(currentIndex != -1)
            {
                _BookStore[currentIndex] = Book;
            }
            else
            {
                _BookStore.Add(Book);
            }

            BookUpdated?.Invoke(Book);
        }

        public async Task Delete(Guid id)
        {
            await _deleteBookCommand.Execute(id);

            _BookStore.RemoveAll(y => y.Id == id);

            BookDeleted?.Invoke(id);
        }
    }
}
