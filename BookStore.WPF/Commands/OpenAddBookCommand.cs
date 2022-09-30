using BookStore.WPF.Stores;
using BookStore.WPF.ViewModels;

namespace BookStore.WPF.Commands
{
    public class OpenAddBookCommand : CommandBase
    {
        private readonly BookStorage _BookStorage;
        private readonly ModalNavigationStorage _modalNavigationStore;

        public OpenAddBookCommand(BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            _BookStorage = BookStorage;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddBookViewModel addBookViewModel = new AddBookViewModel(_BookStorage, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addBookViewModel;
        }
    }
}
