using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    public class LuminositySensor
    {
        private double _infraRed;
        private double _green;
        private double _blue;
        private double _red;
        private double _proximity;

        public double infraRed { get { return _infraRed; } set { _infraRed = value; } }
        public double Green { get { return _green; } set { _green = value; } }
        public double Blue { get { return _blue; } set { _blue = value; } }
        public double Red { get { return _red; } set { _red = value; } }    
        public double Proximity { get { return _proximity; } set { _proximity = value; } } 
    }
}
