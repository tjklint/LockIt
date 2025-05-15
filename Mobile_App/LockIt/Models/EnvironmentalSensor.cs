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
    /// Provides functionality to read environmental data including temperature and humidity.
    /// </summary>
    public class EnvironmentalSensor
    {
        private double _temperatureSensor;
        private double _humiditySensor;
        private LuminositySensor _luminositySensor = new LuminositySensor();


        /// <summary>
        /// Gets or sets the value used for temperature measurements.
        /// </summary>
        // TODO: Add validation
        public double TemperatureSensor
        {
            get { return _temperatureSensor; }
            set { _temperatureSensor = value; }
        }

        /// <summary>
        /// Gets or sets the value used for the luminosity sensor.
        /// </summary>
        public LuminositySensor LuminositySensor
        {
            get { return _luminositySensor; }
            set { _luminositySensor = value; }
        }

        /// <summary>
        /// Gets or sets the value used for the humidity measurements.
        /// </summary>
        public double HumiditySensor
        {
            get { return _humiditySensor; }
            set { _humiditySensor = value; }
        }
    }
}
