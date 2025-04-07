using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfuritySolutions.Models
{
    /// <summary>
    /// Represents a general user of the Comfurity system.
    /// </summary>
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; protected set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Gets the full display name of the user.
        /// </summary>
        public abstract string GetWelcomeMessage();
    }
}
