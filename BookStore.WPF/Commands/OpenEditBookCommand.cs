using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class OpenEditBookCommand : CommandBase
    {
        private readonly BookStoreListingItemViewModel _BookStoreListingItemViewModel;
        private readonly BookStorage _BookStorage;
        private readonly ModalNavigationStorage _modalNavigationStore;

        public OpenEditBookCommand(BookStoreListingItemViewModel BookStoreListingItemViewModel,
            BookStorage BookStorage, 
            ModalNavigationStorage modalNavigationStore)
        {
            _BookStoreListingItemViewModel = BookStoreListingItemViewModel;
            _BookStorage = BookStorage;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Book Book = _BookStoreListingItemViewModel.Book;

            EditBookViewModel editBookViewModel = 
                new EditBookViewModel(Book, _BookStorage, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editBookViewModel;
        }
    }
}
