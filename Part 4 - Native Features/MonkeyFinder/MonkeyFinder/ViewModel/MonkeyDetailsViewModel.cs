using MonkeyFinder.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MonkeyFinder.ViewModel
{
    public class MonkeyDetailsViewModel : BaseViewModel
    {
        public MonkeyDetailsViewModel()
        {
           
        }

        public MonkeyDetailsViewModel(Monkey monkey)
            : this()
        {
            Monkey = monkey;
            Title = $"{Monkey.Name} Details";
        }
        Monkey monkey;
        public Monkey Monkey
        {
            get => monkey;
            set
            {
                if (monkey == value)
                    return;

                monkey = value;
                OnPropertyChanged();
            }
        }
    }
}
