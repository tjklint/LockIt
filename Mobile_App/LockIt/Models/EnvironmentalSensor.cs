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
    internal class EnvironmentalSensor
    {
        private I2cDevice _temperatureHumiditySensor;
        private GpioController _luminositySensor = new GpioController();
        EnvironmentalSensor() 
        {
            I2cConnectionSettings settings = new I2cConnectionSettings(4,0x38);
            _temperatureHumiditySensor = I2cDevice.Create(settings);
        }

        //TODO: Add validation
        public I2cDevice TemperatureHumiditySensor
        {
            get { return _temperatureHumiditySensor; }
            set { _temperatureHumiditySensor = value; }
        }
   
        public GpioController LuminositySensor
        {
            get { return _luminositySensor; }
            set { _luminositySensor = value; }
        }

        public UnitsNet.Temperature GetTemperature()
        {
            using (Aht20 sensor = new Aht20(_temperatureHumiditySensor)) 
            { 
                return sensor.GetTemperature();
            }
        }
        public UnitsNet.RelativeHumidity GetHumidity() 
        {
            using (Aht20 sensor = new Aht20(_temperatureHumiditySensor))
            {
                return sensor.GetHumidity();
            }
        }
        
        //TODO: Figure out how the luminosity sensor will function
        // and what data it will return.
        public void GetLuminosity()
        {

        }
    }
}
