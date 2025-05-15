
using LockIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.DataRepos
{
    internal class UserDataRepo
    {
        public string Username { get; set; } = "AUsername";

        public string Password { get; set; } = "APassword";

        public EnvironmentalSensor EnvironmentalSensor { get; set; } = new EnvironmentalSensor();

        public SecurityModel SecurityData { get; set; } = new SecurityModel(123);

        public float Longitude { get; set; } = 92.90769F;

        public float Lattitude { get; set; } = -84.32806F;

        public string VideoSource { get; set; } = "https://platform.theverge.com/wp-content/uploads/sites/2/chorus/uploads/chorus_asset/file/24488382/batterdoorbellplus_package_deliverypov.jpg?quality=90&strip=all&crop=7.8125%2C0%2C84.375%2C100&w=1080";
    }
}
