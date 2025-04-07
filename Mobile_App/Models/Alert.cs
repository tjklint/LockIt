using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Represents a triggered alert in the system.
    /// </summary>
    public class Alert
    {
        public string AlertMessage { get; set; }
        public DateTime Timestamp { get; set; }

        public Alert(string message)
        {
            AlertMessage = message;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp:HH:mm} - {AlertMessage}";
        }
    }
}
