using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Represents a home owner with partial system access.
    /// </summary>
    public class HomeOwner : User
    {
        public HomeOwner(string username, string password)
            : base(username, password)
        {
            Role = "HomeOwner";
        }

        public override string GetWelcomeMessage()
        {
            return $"Welcome Home Owner, {Username}!";
        }
    }
}
