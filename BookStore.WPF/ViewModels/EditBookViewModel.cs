using System;
using System.Windows.Input;
using BookStore.WPF.Commands;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;

namespace BookStore.WPF.ViewModels
{
    public class EditBookViewModel : ViewModelBase
    {
        public Guid BookId { get; }

        public BookDetailsFormViewModel BookDetailsFormViewModel { get; }

        public EditBookViewModel(Book Book, BookStorage BookStorage, ModalNavigationStorage modalNavigationStore)
        {
            BookId = Book.Id;

            ICommand submitCommand = new EditBookCommand(this, BookStorage, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BookDetailsFormViewModel = new BookDetailsFormViewModel(submitCommand, cancelCommand)
            { 
                Name = Book.Name,
                Author = Book.Author,
                Year = Book.Year,
                Isbn = Book.Isbn,
                Desc = Book.Desc
            };
        }
    }
}
