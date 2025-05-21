
using LockIt.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.DataRepos
{
    public class UserDataRepo
    {
        public string Username { get; set; } = "AUsername";

        public string Password { get; set; } = "APassword";

        public static EnvironmentalSensor EnvironmentalSensor { get; set; } = new EnvironmentalSensor();

        public static SecurityModel SecurityData { get; set; } = new SecurityModel(123);

        public float Longitude { get; set; } = 92.90769F;

        public float Latitude { get; set; } = -84.32806F;

        public uint Motion { get; set; } = 0;

        public string VideoSource { get; set; } = "https://platform.theverge.com/wp-content/uploads/sites/2/chorus/uploads/chorus_asset/file/24488382/batterdoorbellplus_package_deliverypov.jpg?quality=90&strip=all&crop=7.8125%2C0%2C84.375%2C100&w=1080";

        public void UpdateFromJson(JObject json)
        {
            string measurement = json["measurement"]?.ToString();
            var value = json["value"];

            switch (measurement)
            {
                case "temperature in degrees celcius":
                    EnvironmentalSensor.TemperatureSensor = (double)value;
                    break;
                case "relative measurement of humidity":
                    EnvironmentalSensor.HumiditySensor = (double)value;
                    break;
                case "InfraRed Luminance":
                    EnvironmentalSensor.LuminositySensor.infraRed = (double)value;
                    break;
                case "Red Color Luminance":
                    EnvironmentalSensor.LuminositySensor.Red = (double)value;
                    break;
                case "Green Color Luminance":
                    EnvironmentalSensor.LuminositySensor.Green = (double)value;
                    break;
                case "Blue Color Luminance":
                    EnvironmentalSensor.LuminositySensor.Blue = (double)value;
                    break;
                case "Proximity of device.":
                    EnvironmentalSensor.LuminositySensor.Proximity = (double)value;
                    break;
                case "If True Door is closed if False door is open":
                    SecurityData.IsClosed = (bool)value;
                    break;
                case "motion detected (1/0)":
                    Motion = value?.Value<uint>() ?? 0;
                    break;
                case "GPS latitude and longitude":
                    var coords = value?.ToString().Split(',');
                    if (coords?.Length == 2 &&
                        float.TryParse(coords[0], out float lat) &&
                        float.TryParse(coords[1], out float lon))
                    {
                        Latitude = lat;
                        Longitude = lon;
                    }
                    break;
                default:
                    Debug.WriteLine($"Unknown data type: {measurement}");
                    break;
            }
        }
    }
}
