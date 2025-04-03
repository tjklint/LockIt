using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    internal class Surveillance
    {
        private int _camera;
        private int _motionSensor;
        private int _GPS;
        public Surveillance() { }
        public int Camera { get { return _camera; } 
            set {
                //Add validation
                _camera = value;
            } }
        public int MotionSensor
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


    }
}
