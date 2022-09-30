using System.Windows;
using System.Windows.Controls;

namespace BookStore.WPF.Components
{
    /// <summary>
    /// Interaction logic for BookStoreListingItem.xaml
    /// </summary>
    public partial class BookStoreListingItem : UserControl
    {
        public BookStoreListingItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        }
    }
}
