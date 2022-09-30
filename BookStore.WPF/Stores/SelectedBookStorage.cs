using System;
using BookStore.Domain.Models;

namespace BookStore.WPF.Stores
{
    public class SelectedBookStorage
    {
        private readonly BookStorage _BookStorage;

        private Book _selectedBook;
        public Book SelectedBook
        {
            get
            {
                return _selectedBook;
            }
            set
            {
                _selectedBook = value;
                SelectedBookChanged?.Invoke();
            }
        }

        public event Action SelectedBookChanged;

        public SelectedBookStorage(BookStorage BookStorage)
        {
            _BookStorage = BookStorage;

            _BookStorage.BookAdded += BookStorage_BookAdded;
            _BookStorage.BookUpdated += BookStorage_BookUpdated;
        }

        private void BookStorage_BookAdded(Book Book)
        {
            SelectedBook = Book;
        }

        private void BookStorage_BookUpdated(Book Book)
        {
            if(Book.Id == SelectedBook?.Id)
            {
                SelectedBook = Book;
            }
        }
    }
}
