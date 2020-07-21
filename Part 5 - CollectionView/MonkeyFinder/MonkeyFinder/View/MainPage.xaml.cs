using MonkeyFinder.Model;
using MonkeyFinder.ViewModel;
using System.Linq;
using Xamarin.Forms;

namespace MonkeyFinder.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MonkeysViewModel();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var monkey = e.CurrentSelection.FirstOrDefault() as Monkey;
            if (monkey == null)
                return;

            await Navigation.PushAsync(new DetailsPage(monkey));

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
