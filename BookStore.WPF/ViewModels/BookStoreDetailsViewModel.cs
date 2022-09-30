using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using System;
using System.Drawing;

namespace BookStore.WPF.ViewModels
{
    public class BookStoreDetailsViewModel : ViewModelBase 
    {
        private readonly SelectedBookStorage _SelectedBookStorage;

        private Book SelectedBook => _SelectedBookStorage.SelectedBook;

        public bool HasSelectedBook => SelectedBook != null;
        public string Id => SelectedBook?.Id.ToString() ?? "Unknown";
        public string Name => SelectedBook?.Name ?? "Unknown";
        public string Author => SelectedBook?.Author ?? "Unknown";
        public string Year => SelectedBook?.Year ?? "Unknown";
        public string Isbn => SelectedBook?.Isbn ?? "Unknown";
        //public string Image => SelectedBook?.Image;
        public string Desc => SelectedBook?.Desc ?? "Unknown";

        public BookStoreDetailsViewModel(SelectedBookStorage SelectedBookStorage)
        {
            _SelectedBookStorage = SelectedBookStorage;

            _SelectedBookStorage.SelectedBookChanged += SelectedBookStorage_SelectedBookChanged;
        }

        protected override void Dispose()
        {
            _SelectedBookStorage.SelectedBookChanged -= SelectedBookStorage_SelectedBookChanged;

            base.Dispose();
        }

        private void SelectedBookStorage_SelectedBookChanged()
        {
            OnPropertyChanged(nameof(HasSelectedBook));
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Isbn));
            //OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(Desc));
        }
    }
}
