using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Represents a business owner with full system access.
    /// </summary>
    public class BusinessOwner : User
    {
        public BusinessOwner(string username, string password)
            : base(username, password)
        {
            Role = "BusinessOwner";
        }

        public override string GetWelcomeMessage()
        {
            return $"Welcome Business Owner, {Username}!";
        }
    }
}
