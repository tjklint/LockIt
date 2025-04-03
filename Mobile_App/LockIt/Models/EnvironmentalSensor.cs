using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;
namespace LockIt.Models
{
    internal class EnvironmentalSensor
    {
        private int _temperatureSensor;
        private int _humiditySensor;
        private int _luminositySensor;
        EnvironmentalSensor() { }

        public int TemperatureSensor
        {
            get { return _temperatureSensor; }
            set { _temperatureSensor = value; }
        }
        public int HumiditySensor
        {
            get { return _humiditySensor; }
            set { _humiditySensor = value; }
        }
        public int LuminositySensor
        {
            get { return _luminositySensor; }
            set { _luminositySensor = value; }
        }

        public int GetTemperature()
        {
            return _temperatureSensor;
        }
        public int GetHumidity() 
        {
            return _humiditySensor;
        }
    }
}
