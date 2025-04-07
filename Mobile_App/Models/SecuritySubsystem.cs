using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Controls security-related components such as motion and door sensors.
    /// </summary>
    public class SecuritySubsystem
    {
        public List<Sensor> Sensors { get; set; } = new();
        public List<Actuator> Actuators { get; set; } = new();

        public SecuritySubsystem(List<Sensor> sensors, List<Actuator> actuators)
        {
            Sensors = sensors;
            Actuators = actuators;
        }

        /// <summary>
        /// Evaluates security sensor data and triggers alarms or locks.
        /// </summary>
        public void EvaluateSecurity()
        {
            foreach (var sensor in Sensors)
            {
                //if (sensor.Type == "Motion" && sensor.Value > 0)
                //{

                //}
            }
        }

        // TODO
        // CREATE METHODS THAT ANALYZE SPECIFIC SENSORS AND USES THE ACTUATORS
    }
}
