using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    /// <summary>
    /// Controls locking and unlocking mechanisms using GPIO, and includes preliminary support for motor control.
    /// </summary>
    internal class Security
    {
        private int _lockPin;
        private GpioController _lock = new GpioController();
        private GpioController _motor = new GpioController();
        private bool isLocked = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class with a specified GPIO pin for lock control.
        /// </summary>
        /// <param name="lockPin">The GPIO pin number used for locking and unlocking.</param>
        public Security(int lockPin)
        {
            _lockPin = lockPin;
        }

        /// <summary>
        /// Gets or sets the GPIO controller responsible for lock operations.
        /// </summary>
        public GpioController Lock
        {
            get { return _lock; }
            set { _lock = value; }
        }

        /// <summary>
        /// Gets or sets the GPIO controller assigned to motor operations.
        /// </summary>
        // TODO: Add validation
        public GpioController Motor
        {
            get { return _motor; }
            set { _motor = value; }
        }

        /// <summary>
        /// Unlocks the device by writing a high value to the lock pin.
        /// </summary>
        /// <example>
        /// <code>
        /// var security = new Security(21);
        /// security.Unlocking();
        /// </code>
        /// </example>
        public void Unlocking()
        {
            using (_lock)
            {
                _lock.OpenPin(_lockPin, PinMode.Output);
                _lock.Write(_lockPin, PinValue.High);
                isLocked = false;
            }
        }

        /// <summary>
        /// Locks the device by writing a low value to the lock pin.
        /// </summary>
        /// <example>
        /// <code>
        /// var security = new Security(21);
        /// security.Locking();
        /// </code>
        /// </example>
        public void Locking()
        {
            using (_lock)
            {
                _lock.OpenPin(_lockPin, PinMode.Output);
                _lock.Write(_lockPin, PinValue.Low);
                isLocked = true;
            }
        }

        // TODO: Figure out how to get the data/properly use the Motor.
        public void GetMotor()
        {

        }
    }
}
