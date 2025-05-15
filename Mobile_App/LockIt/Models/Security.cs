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
    public class SecurityModel
    {
        private int _lockPin;
        private bool _isLocked;
        private bool _isClosed;
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityModel"/> class with a specified GPIO pin for lock control.
        /// </summary>
        /// <param name="lockPin">The GPIO pin number used for locking and unlocking.</param>
        public SecurityModel(int lockPin)
        {
            _lockPin = lockPin;
        }

        /// <summary>
        /// Gets or sets the boolean for whether the door is locked or not.
        /// </summary>
        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        /// <summary>
        /// Gets or sets the boolean for whether the door is closed or not.
        /// </summary>
        public bool IsClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; }
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
          
        }

    }
}
