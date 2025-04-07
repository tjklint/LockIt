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
                new Alert("Door 1 unlocked @ 08:30 AM"),
                new Alert ("Motion detected in hallway")
            });
        }

        public Task<List<Alert>> GetComfortAlertsAsync()
        {
            return Task.FromResult(new List<Alert>
            {
                new Alert ("Temperature at 26°C"),
                new Alert ("Humidity reached 60%")
            });
        }
    }
}
