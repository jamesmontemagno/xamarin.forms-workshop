using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using System.Linq;
using MonkeyFinder.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MonkeyFinder.ViewModel
{
    public class MonkeysViewModel : BaseViewModel
    {
        public Command GetClosestCommand { get; }
        public Command GetMonkeysCommand { get; }
        public ObservableCollection<Monkey> Monkeys { get; }
        public MonkeysViewModel()
        {
            Title = "Monkey Finder";
            Monkeys = new ObservableCollection<Monkey>();
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
            GetClosestCommand = new Command(async () => await GetClosestAsync());
        }
        async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;


                // If having internet issues, read from disk
                //string json = null;
                //var a = System.Reflection.Assembly.GetExecutingAssembly();
                //using (var resFilestream = a.GetManifestResourceStream("MonkeyFinder.monkeydata.json"))
                //{
                //    using (var reader = new System.IO.StreamReader(resFilestream))
                //        json = await reader.ReadToEndAsync();
                //}

                // if internet is working
                var client = new HttpClient();
                var json = await client.GetStringAsync("https://montemagno.com/monkeys.json");

                var monkeys = JsonConvert.DeserializeObject<List<Monkey>>(json);
                
                Monkeys.Clear();
                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetClosestAsync()
        {
            if (IsBusy || Monkeys.Count == 0)
                return;

            try
            {
                // Get cached location, else get real location.
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                // Find closest monkey to us
                var first = Monkeys.OrderBy(m => location.CalculateDistance(
                    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await Application.Current.MainPage.DisplayAlert("", first.Name + " " +
                    first.Location, "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
