using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class MainListPage : ContentPage
    {
        public MainListPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(sender is ListView list) {
                list.SelectedItem = null;
            }
        }
    }
}
