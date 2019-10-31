using MonkeyFinder.Model;
using MonkeyFinder.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyFinder.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
        public DetailsPage(Monkey monkey)
        {
            InitializeComponent();
            BindingContext = new MonkeyDetailsViewModel(monkey);
        }
    }
}