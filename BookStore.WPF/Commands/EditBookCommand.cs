using System;
using System.Threading.Tasks;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class EditBookCommand : AsyncCommandBase
    {
        private readonly EditBookViewModel _editBookViewModel;
        private readonly BookStorage _BookStorage;
        private readonly ModalNavigationStorage _modalNavigationStore;

        public EditBookCommand(EditBookViewModel editBookViewModel, BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            _editBookViewModel = editBookViewModel;
            _BookStorage = BookStorage;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BookDetailsFormViewModel formViewModel = _editBookViewModel.BookDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            Book Book = new Book(
                _editBookViewModel.BookId,
                formViewModel.Name,
                formViewModel.Author,
                formViewModel.Year,
                formViewModel.Isbn,
                formViewModel.Desc);

            try
            {
                await _BookStorage.Update(Book);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update Book. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
