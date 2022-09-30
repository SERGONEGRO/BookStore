using BookStore.WPF.Stores;

namespace BookStore.WPF.Commands
{
    public class CloseModalCommand : CommandBase
    {
        private readonly ModalNavigationStorage _modalNavigationStore;

        public CloseModalCommand(ModalNavigationStorage modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
