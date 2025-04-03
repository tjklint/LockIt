using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    internal class Security
    {
        private int _lockPin;
        private GpioController _lock = new GpioController();
        private GpioController _motor = new GpioController();
        private bool isLocked = false;  
        public Security(int lockPin) 
        { 
            _lockPin = lockPin;
        }

        public GpioController Lock { get { return _lock; }
            set { _lock = value; }  
        
        }

        //TODO: Add validation
        public GpioController Motor
        {
            get { return _motor; }
            set { _motor = value; }

        }

        public void Unlocking()
        {
            using (_lock)
            {
                _lock.OpenPin(_lockPin, PinMode.Output);
                _lock.Write(_lockPin, PinValue.High);
                isLocked = false;
            }
        }
        public void Locking()
        {
            using (_lock)
            {
                _lock.OpenPin(_lockPin, PinMode.Output);
                _lock.Write(_lockPin, PinValue.Low);
                isLocked = true;
            }
        }
        //TODO: Figure out how to get the data/properly use the Motor.
        public void GetMotor()
        {

        }
    }
}
