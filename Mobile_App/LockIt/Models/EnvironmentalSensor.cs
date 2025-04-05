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
    internal class EnvironmentalSensor
    {
        private I2cDevice _temperatureHumiditySensor;
        private GpioController _luminositySensor = new GpioController();

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentalSensor"/> class,
        /// and sets up the I2C connection for the temperature and humidity sensor.
        /// </summary>
        EnvironmentalSensor()
        {
            I2cConnectionSettings settings = new I2cConnectionSettings(4, 0x38);
            _temperatureHumiditySensor = I2cDevice.Create(settings);
        }

        /// <summary>
        /// Gets or sets the I2C device used for temperature and humidity measurements.
        /// </summary>
        // TODO: Add validation
        public I2cDevice TemperatureHumiditySensor
        {
            get { return _temperatureHumiditySensor; }
            set { _temperatureHumiditySensor = value; }
        }

        /// <summary>
        /// Gets or sets the GPIO controller used for the luminosity sensor.
        /// </summary>
        public GpioController LuminositySensor
        {
            get { return _luminositySensor; }
            set { _luminositySensor = value; }
        }

        /// <summary>
        /// Retrieves the current ambient temperature using the AHT20 sensor.
        /// </summary>
        /// <returns>The current temperature as a <see cref="UnitsNet.Temperature"/> object.</returns>
        /// <example>
        /// <code>
        /// var sensor = new EnvironmentalSensor();
        /// var temp = sensor.GetTemperature();
        /// Console.WriteLine(temp.DegreesCelsius + " °C");
        /// </code>
        /// </example>
        public UnitsNet.Temperature GetTemperature()
        {
            using (Aht20 sensor = new Aht20(_temperatureHumiditySensor))
            {
                return sensor.GetTemperature();
            }
        }

        /// <summary>
        /// Retrieves the current relative humidity using the AHT20 sensor.
        /// </summary>
        /// <returns>The current humidity as a <see cref="UnitsNet.RelativeHumidity"/> object.</returns>
        /// <example>
        /// <code>
        /// var sensor = new EnvironmentalSensor();
        /// var humidity = sensor.GetHumidity();
        /// Console.WriteLine(humidity.Percent + " %");
        /// </code>
        /// </example>
        public UnitsNet.RelativeHumidity GetHumidity()
        {
            using (Aht20 sensor = new Aht20(_temperatureHumiditySensor))
            {
                return sensor.GetHumidity();
            }
        }

        // TODO: Figure out how the luminosity sensor will function
        // and what data it will return.
        public void GetLuminosity()
        {

        }
    }
}
