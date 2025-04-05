using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Models
{
    /// <summary>
    /// Represents the response returned from Firebase authentication operations.
    /// </summary>
    public class FirebaseAuthResponse
    {
        /// <summary>
        /// Gets or sets the Firebase ID token for the authenticated user.
        /// </summary>
        public string idToken { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the authenticated user.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the refresh token that can be used to obtain new ID tokens.
        /// </summary>
        public string refreshToken { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds until the ID token expires.
        /// </summary>
        public string expiresIn { get; set; }

        /// <summary>
        /// Gets or sets the unique user ID (local ID) assigned by Firebase.
        /// </summary>
        public string localId { get; set; }

        /// <summary>
        /// Gets or sets the error message returned by Firebase, if the authentication request fails.
        /// </summary>
        public string Error { get; set; }
    }
}
