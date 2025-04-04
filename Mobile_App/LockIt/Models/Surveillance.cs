using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;

namespace LockIt.Models
{
    /// <summary>
    /// Provides functionality for handling motion detection, camera feed access, and GPS data retrieval.
    /// </summary>
    internal class Surveillance
    {
        private int _motionSensorPin;

        // TODO: Figure out how the data from the camera will be used.
        private int _camera;

        private GpioController _motionSensor = new GpioController();
        private int _GPS;

        /// <summary>
        /// Initializes a new instance of the <see cref="Surveillance"/> class with the specified GPIO pin for motion detection.
        /// </summary>
        /// <param name="motionSensorPin">The GPIO pin connected to the motion sensor.</param>
        // TODO: Add validation
        public Surveillance(int motionSensorPin)
        {
            _motionSensorPin = motionSensorPin;
        }

        /// <summary>
        /// Gets or sets the identifier for the camera device.
        /// </summary>
        public int Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        /// <summary>
        /// Gets or sets the GPIO controller used for the motion sensor.
        /// </summary>
        public GpioController MotionSensor
        {
            get { return _motionSensor; }
            set { _motionSensor = value; }
        }

        /// <summary>
        /// Gets or sets the GPS data reference.
        /// </summary>
        public int GPS
        {
            get { return _GPS; }
            set { _GPS = value; }
        }

        /// <summary>
        /// Detects whether motion has been triggered via the motion sensor.
        /// </summary>
        /// <returns><c>true</c> if motion is detected; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var surveillance = new Surveillance(17);
        /// bool motionDetected = surveillance.IsMotion();
        /// </code>
        /// </example>
        public bool IsMotion()
        {
            _motionSensor.OpenPin(_motionSensorPin, PinMode.Input);
            if (_motionSensor.Read(_motionSensorPin) == PinValue.High)
            {
                Console.WriteLine("Motion detected");
                return true;
            }
            else
            {
                Console.WriteLine("No Motion detected");
                return false;
            }
        }

        // TODO: Figure out how to get GPS related data from the Raspberry Pi.
        public void GetGPSData()
        {

        }

        /// <summary>
        /// Retrieves the first available video capture device (e.g., camera).
        /// </summary>
        /// <returns>A <see cref="VideoCaptureDevice"/> object representing the camera, or <c>null</c> if none found.</returns>
        /// <example>
        /// <code>
        /// var surveillance = new Surveillance(17);
        /// var camera = surveillance.GetCamera();
        /// if (camera != null)
        /// {
        ///     camera.Start();
        /// }
        /// </code>
        /// </example>
        public VideoCaptureDevice GetCamera()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                Console.WriteLine("No camera found.");
                return null;
            }
            else
            {
                return new VideoCaptureDevice(videoDevices[0].MonikerString);
            }
        }
    }
}
