using System;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class AddBookCommand : AsyncCommandBase
    {
        private readonly AddBookViewModel _addBookViewModel;
        private readonly BookStorage _BookStorage;
        private readonly ModalNavigationStorage _modalNavigationStore;

        public AddBookCommand(AddBookViewModel addBookViewModel, BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            _addBookViewModel = addBookViewModel;
            _BookStorage = BookStorage;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BookDetailsFormViewModel formViewModel = _addBookViewModel.BookDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            Book Book = new Book(
                Guid.NewGuid(),
                formViewModel.Name,
                formViewModel.Author,
                formViewModel.Year,
                formViewModel.Isbn,
                formViewModel.Desc);

            try
            {
                await _BookStorage.Add(Book);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add Book. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
