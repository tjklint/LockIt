// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Represents luminosity sensor data including RGB channels, infrared, and proximity values.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    /// <summary>
    /// Represents readings from a luminosity sensor, including RGB, infrared, and proximity data.
    /// </summary>
    public class LuminositySensor
    {
        private double _infraRed;
        private double _green;
        private double _blue;
        private double _red;
        private double _proximity;

        /// <summary>
        /// Gets or sets the infrared light intensity value.
        /// </summary>
        public double infraRed
        {
            get { return _infraRed; }
            set { _infraRed = value; }
        }

        /// <summary>
        /// Gets or sets the green light intensity value.
        /// </summary>
        public double Green
        {
            get { return _green; }
            set { _green = value; }
        }

        /// <summary>
        /// Gets or sets the blue light intensity value.
        /// </summary>
        public double Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }

        /// <summary>
        /// Gets or sets the red light intensity value.
        /// </summary>
        public double Red
        {
            get { return _red; }
            set { _red = value; }
        }

        /// <summary>
        /// Gets or sets the proximity value indicating how close an object is to the sensor.
        /// </summary>
        public double Proximity
        {
            get { return _proximity; }
            set { _proximity = value; }
        }
    }
}
