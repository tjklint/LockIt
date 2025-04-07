using ComfuritySolutions.Models;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComfuritySolutions.ViewModels
{
    public class HomePageViewModel
    {
        public string WelcomeMessage { get; set; }

        public ObservableCollection<Alert> SecurityAlerts { get; set; }
        public ObservableCollection<Alert> ComfortAlerts { get; set; }

        public ICommand NavigateCommand { get; }

        public HomePageViewModel()
        {
            WelcomeMessage = $"Welcome back, Youmna!";

            // Example data
            SecurityAlerts = new ObservableCollection<Alert>
        {
            new Alert { AlertMessage = "Door 1 unlocked @ 8:30AM" },
            new Alert { AlertMessage = "Motion detected in hallway" }
        };

            ComfortAlerts = new ObservableCollection<Alert>
        {
            new Alert { AlertMessage = "Temperature 25°C" },
            new Alert { AlertMessage = "Humidity 60%" }
        };

            NavigateCommand = new Command<string>(OnNavigate);
        }

        private async void OnNavigate(string destination)
        {
            if (!string.IsNullOrWhiteSpace(destination))
            {
                await Shell.Current.GoToAsync($"//{destination}");
            }
        }
    }

    public class Alert
    {
        public string AlertMessage { get; set; }
    }
}
