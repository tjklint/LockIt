// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: ViewModel for binding and updating environmental sensor data for the menu pages.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using LockIt.Models;

namespace LockIt.ViewModels
{
    /// <summary>
    /// ViewModel that holds environmental sensor data and updates it based on incoming JSON events.
    /// </summary>
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the environmental sensor data model.
        /// </summary>
        public EnvironmentalSensor Environment { get; set; } = new();

        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the email of the associated homeowner (used in visitor context).
        /// </summary>
        public string HomeownerEmail { get; set; }

        /// <summary>
        /// Updates the environmental data properties from a JSON event payload.
        /// </summary>
        /// <param name="json">A JSON object containing a measurement and value.</param>
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

        /// <summary>
        /// Notifies the UI that a property has changed.
        /// </summary>
        /// <param name="name">The name of the property. Automatically supplied by the caller if not provided.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
