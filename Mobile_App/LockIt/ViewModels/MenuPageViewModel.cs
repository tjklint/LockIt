using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using LockIt.Models;

namespace LockIt.ViewModels
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        public EnvironmentalSensor Environment { get; set; } = new();
        public event PropertyChangedEventHandler PropertyChanged;
        public string HomeownerEmail { get; set; }


        public void UpdateData(JObject json)
        {
            var measurement = json["measurement"]?.ToString();
            var value = json["value"]?.ToString();

            if (!double.TryParse(value, out double parsedValue))
                return;

            switch (measurement)
            {
                case "temperature in degrees celsius":
                    Environment.TemperatureSensor = parsedValue;
                    break;
                case "relative measurement of humidity":
                    Environment.HumiditySensor = parsedValue;
                    break;
                case "Red Color Luminance":
                    Environment.LuminositySensor.Red = parsedValue;
                    break;
                case "Green Color Luminance":
                    Environment.LuminositySensor.Green = parsedValue;
                    break;
                case "Blue Color Luminance":
                    Environment.LuminositySensor.Blue = parsedValue;
                    break;
                case "Proximity of device.":
                    Environment.LuminositySensor.Proximity = parsedValue;
                    break;
            }

            OnPropertyChanged(nameof(Environment));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
