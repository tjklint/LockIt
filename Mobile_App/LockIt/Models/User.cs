using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    /// <summary>
    /// Represents a user authenticated through Firebase.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier (UID) assigned to the user by Firebase.
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the user.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
