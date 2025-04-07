using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Controls devices and sensors related to comfort (temperature, humidity).
    /// </summary>
    public class ComfortSubsystem
    {
        public List<Sensor> Sensors { get; set; } = new();
        public List<Actuator> Actuators { get; set; } = new();

        public ComfortSubsystem(List<Sensor> sensors_, List<Actuator> actuators_)
        {
            Sensors = sensors_;
            Actuators = actuators_;
        }

        /// <summary>
        /// Evaluates comfort sensor data and triggers alarms or fans.
        /// </summary>
        public void EvaluateComfort()
        {
            foreach (var sensor in Sensors)
            {
                //if (sensor.Type == "Temperature" && sensor.Value > 25)
                //{
                    
                //}
            }
        }

        // TODO
        // CREATE METHODS THAT ANALYZE SPECIFIC SENSORS AND USES THE ACTUATORS
    }
}
