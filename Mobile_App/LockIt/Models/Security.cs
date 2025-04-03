using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    internal class Security
    {
        private int _lock;
        private int _motor;
        public Security() { }

        public int Lock { get { return _lock; }
            set { _lock = value; }  
        
        }

        public int Motor
        {
            get { return _motor; }
            set { _motor = value; }

        }
    }
}
