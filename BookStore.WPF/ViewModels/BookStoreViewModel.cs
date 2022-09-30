using System.Windows.Input;
using BookStore.WPF.Commands;
using BookStore.WPF.Stores;

namespace BookStore.WPF.ViewModels
{
    public class BookStoreViewModel : ViewModelBase
    {
        public BookStoreListingViewModel BookStoreListingViewModel { get; }
        public BookStoreDetailsViewModel BookStoreDetailsViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        public ICommand LoadBookStoreCommand { get; }
        public ICommand AddBookStoreCommand { get; }

        public BookStoreViewModel(BookStorage BookStorage, SelectedBookStorage SelectedBookStorage, ModalNavigationStorage modalNavigationStore)
        {
            BookStoreListingViewModel = new BookStoreListingViewModel(BookStorage, SelectedBookStorage, modalNavigationStore);
            BookStoreDetailsViewModel = new BookStoreDetailsViewModel(SelectedBookStorage);

            LoadBookStoreCommand = new LoadBookStoreCommand(this, BookStorage);
            AddBookStoreCommand = new OpenAddBookCommand(BookStorage, modalNavigationStore);
        }

        public static BookStoreViewModel LoadViewModel(BookStorage BookStorage, SelectedBookStorage SelectedBookStorage, ModalNavigationStorage modalNavigationStore)
        {
            BookStoreViewModel viewModel = new BookStoreViewModel(BookStorage, SelectedBookStorage, modalNavigationStore);

            viewModel.LoadBookStoreCommand.Execute(null);

            return viewModel;
        }
    }
}
