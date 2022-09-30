using System.Windows.Input;
using BookStore.WPF.Commands;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;

namespace BookStore.WPF.ViewModels
{
    public class BookStoreListingItemViewModel : ViewModelBase
    {
        public Book Book { get; private set; }

        public string Name => Book.Name;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookStoreListingItemViewModel(Book Book, BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            this.Book = Book;

            EditCommand = new OpenEditBookCommand(this, BookStorage, modalNavigationStore);
            DeleteCommand = new DeleteBookCommand(this, BookStorage);
        }

        public void Update(Book Book)
        {
            this.Book = Book;

            OnPropertyChanged(nameof(Name));
        }
    }
}
