using System;
using System.Threading.Tasks;
using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class LoadBookStoreCommand : AsyncCommandBase
    {
        private readonly BookStoreViewModel _BookStoreViewModel;
        private readonly BookStorage _BookStorage;

        public LoadBookStoreCommand(BookStoreViewModel BookStoreViewModel, BookStorage BookStorage)
        {
            _BookStoreViewModel = BookStoreViewModel;
            _BookStorage = BookStorage;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _BookStoreViewModel.ErrorMessage = null;
            _BookStoreViewModel.IsLoading = true;

            try
            {
                await _BookStorage.Load();
            }
            catch (Exception)
            {
                _BookStoreViewModel.ErrorMessage = "Failed to load Books. Please restart the application.";
            }
            finally
            {
                _BookStoreViewModel.IsLoading = false;
            }
        }
    }
}
