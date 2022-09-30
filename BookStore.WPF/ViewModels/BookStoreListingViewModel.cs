using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BookStore.Domain.Models;
using BookStore.WPF.Stores;
using System.Collections.Specialized;

namespace BookStore.WPF.ViewModels
{
    public class BookStoreListingViewModel : ViewModelBase
    {
        private readonly BookStorage _BookStorage;
        private readonly SelectedBookStorage _SelectedBookStorage;
        private readonly ModalNavigationStorage _modalNavigationStore;

        private readonly ObservableCollection<BookStoreListingItemViewModel> _BookStoreListingItemViewModels;
        public IEnumerable<BookStoreListingItemViewModel> BookStoreListingItemViewModels => _BookStoreListingItemViewModels;

        public BookStoreListingItemViewModel SelectedBookListingItemViewModel
        {
            get
            {
                return _BookStoreListingItemViewModels
                    .FirstOrDefault(y => y.Book?.Id == _SelectedBookStorage.SelectedBook?.Id);
            }
            set
            {
                _SelectedBookStorage.SelectedBook = value?.Book;
            }
        }

        public BookStoreListingViewModel(BookStorage BookStorage, SelectedBookStorage SelectedBookStorage, ModalNavigationStorage modalNavigationStore)
        {
            _BookStorage = BookStorage;
            _SelectedBookStorage = SelectedBookStorage;
            _modalNavigationStore = modalNavigationStore;
            _BookStoreListingItemViewModels = new ObservableCollection<BookStoreListingItemViewModel>();

            _SelectedBookStorage.SelectedBookChanged += SelectedBookStorage_SelectedBookChanged;

            _BookStorage.BookStoreLoaded += BookStorage_BookStoreLoaded;
            _BookStorage.BookAdded += BookStorage_BookAdded;
            _BookStorage.BookUpdated += BookStorage_BookUpdated;
            _BookStorage.BookDeleted += BookStorage_BookDeleted;

            _BookStoreListingItemViewModels.CollectionChanged += BookStoreListingItemViewModels_CollectionChanged;
        }

        protected override void Dispose()
        {
            _SelectedBookStorage.SelectedBookChanged -= SelectedBookStorage_SelectedBookChanged;

            _BookStorage.BookStoreLoaded -= BookStorage_BookStoreLoaded;
            _BookStorage.BookAdded -= BookStorage_BookAdded;
            _BookStorage.BookUpdated -= BookStorage_BookUpdated;
            _BookStorage.BookDeleted -= BookStorage_BookDeleted;

            base.Dispose();
        }

        private void SelectedBookStorage_SelectedBookChanged()
        {
            OnPropertyChanged(nameof(SelectedBookListingItemViewModel));
        }

        private void BookStorage_BookStoreLoaded()
        {
            _BookStoreListingItemViewModels.Clear();

            foreach (Book Book in _BookStorage.BookStore)
            {
                AddBook(Book);
            }
        }

        private void BookStorage_BookAdded(Book Book)
        {
            AddBook(Book);
        }

        private void BookStorage_BookUpdated(Book Book)
        {
            BookStoreListingItemViewModel BookViewModel = 
                _BookStoreListingItemViewModels.FirstOrDefault(y => y.Book.Id == Book.Id);

            if(BookViewModel != null)
            {
                BookViewModel.Update(Book);
            }
        }

        private void BookStorage_BookDeleted(Guid id)
        {
            BookStoreListingItemViewModel itemViewModel = _BookStoreListingItemViewModels.FirstOrDefault(y => y.Book?.Id == id);

            if(itemViewModel != null)
            {
                _BookStoreListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void BookStoreListingItemViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedBookListingItemViewModel));
        }

        private void AddBook(Book Book)
        {
            BookStoreListingItemViewModel itemViewModel = 
                new BookStoreListingItemViewModel(Book, _BookStorage, _modalNavigationStore);
            _BookStoreListingItemViewModels.Add(itemViewModel);
        }
    }
}
