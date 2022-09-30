using System.Windows.Input;
using BookStore.WPF.Commands;
using BookStore.WPF.Stores;

namespace BookStore.WPF.ViewModels
{
    public class AddBookViewModel : ViewModelBase
    {
        public BookDetailsFormViewModel BookDetailsFormViewModel { get; }

        public AddBookViewModel(BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            ICommand submitCommand = new AddBookCommand(this, BookStorage, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BookDetailsFormViewModel = new BookDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
