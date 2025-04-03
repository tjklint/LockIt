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
    internal class Surveillance
    {

        private int _motionSensorPin;
        private int _camera;
        private GpioController _motionSensor = new GpioController();
        private int _GPS;

        //TODO: Add validation
        public Surveillance(int motionSensorPin) 
        { 
           _motionSensorPin = motionSensorPin;  
        }
        public int Camera { get { return _camera; } 
            set {
               
                _camera = value;
            } }
        public GpioController MotionSensor
        {
            get { return _motionSensor; }
            set
            {
                _motionSensor = value;

            }
        }
        public int GPS
        {
            get { return _GPS; }
            set
            {
                _GPS = value;

            }
        }

        public bool IsMotion()
        {
            _motionSensor.OpenPin(_motionSensorPin, PinMode.Input);
            if ( _motionSensor.Read(_motionSensorPin) == PinValue.High)
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

        //TODO: Figure out how to get GPS related data from the rapsberry Pi.
        public void GetGPSData()
        {

        }

  
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
