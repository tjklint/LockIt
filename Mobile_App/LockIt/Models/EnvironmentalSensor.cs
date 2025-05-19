// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Model for capturing environmental sensor readings, including temperature, humidity, and luminosity.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;
using Iot.Device.Ahtxx;
using System.Device.I2c;

namespace LockIt.Models
{
    /// <summary>
    /// Provides functionality to read and store environmental data such as temperature, humidity, and luminosity.
    /// </summary>
    public class EnvironmentalSensor
    {
        private double _temperatureSensor;
        private double _humiditySensor;
        private LuminositySensor _luminositySensor = new LuminositySensor();

        /// <summary>
        /// Gets or sets the temperature reading in degrees Celsius.
        /// </summary>
        public double TemperatureSensor
        {
            get { return _temperatureSensor; }
            set { _temperatureSensor = value; }
        }

        /// <summary>
        /// Gets or sets the sensor used to store RGB and proximity luminosity data.
        /// </summary>
        public LuminositySensor LuminositySensor
        {
            get { return _luminositySensor; }
            set { _luminositySensor = value; }
        }

        /// <summary>
        /// Gets or sets the humidity reading as a percentage.
        /// </summary>
        public double HumiditySensor
        {
            get { return _humiditySensor; }
            set { _humiditySensor = value; }
        }
    }
}
