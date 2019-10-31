using MonkeyFinder.Model;
using Xamarin.Forms;

namespace MonkeyFinder.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var monkey = e.SelectedItem as Monkey;
            if (monkey == null)
                return;

            await Navigation.PushAsync(new DetailsPage(monkey));

            ((ListView)sender).SelectedItem = null;
        }
    }
}
