using ComfuritySolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Repos
{
    public class MockAlert
    {
        public Task<List<Alert>> GetSecurityAlertsAsync()
        {
            return Task.FromResult(new List<Alert>
        {
            new Alert { AlertMessage = "Door 1 unlocked @ 08:30 AM" },
            new Alert { AlertMessage = "Motion detected in hallway" }
        });
        }

        public Task<List<Alert>> GetComfortAlertsAsync()
        {
            return Task.FromResult(new List<Alert>
        {
            new Alert { AlertMessage = "Temperature at 26°C" },
            new Alert { AlertMessage = "Humidity reached 60%" }
        });
        }
    }
}
