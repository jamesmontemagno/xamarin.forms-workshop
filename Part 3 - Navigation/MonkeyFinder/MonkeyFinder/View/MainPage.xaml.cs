using MonkeyFinder.Model;
using MonkeyFinder.ViewModel;
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
    }
}
