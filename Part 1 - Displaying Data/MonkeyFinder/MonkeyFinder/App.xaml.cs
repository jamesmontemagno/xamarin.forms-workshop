using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using MonkeyFinder.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MonkeyFinder
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {

            On<Windows>().SetImageDirectory("Assets");

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Resources["Primary"],
                BarTextColor = Color.White
            };
        }

        const string AppCenteriOS = "";
        const string AppCenterAndroid = "";

        protected override void OnStart()
        {
            if (!string.IsNullOrWhiteSpace(AppCenteriOS) && !string.IsNullOrWhiteSpace(AppCenterAndroid))
            {
                AppCenter.Start($"ios={AppCenteriOS};" +
                      $"android={AppCenterAndroid}",
                      typeof(Analytics), typeof(Crashes));
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
