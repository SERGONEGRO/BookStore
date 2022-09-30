using System;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class DeleteBookCommand : AsyncCommandBase
    {
        private readonly BookStoreListingItemViewModel _BookStoreListingItemViewModel;
        private readonly BookStorage _BookStorage;

        public DeleteBookCommand(BookStoreListingItemViewModel BookStoreListingItemViewModel, BookStorage BookStorage)
        {
            _BookStoreListingItemViewModel = BookStoreListingItemViewModel;
            _BookStorage = BookStorage;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _BookStoreListingItemViewModel.ErrorMessage = null;
            _BookStoreListingItemViewModel.IsDeleting = true;

            Book Book = _BookStoreListingItemViewModel.Book;

            try
            {
                await _BookStorage.Delete(Book.Id);
            }
            catch (Exception)
            {
                _BookStoreListingItemViewModel.ErrorMessage = "Failed to delete Book. Please try again later.";
            }
            finally
            {
                _BookStoreListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
