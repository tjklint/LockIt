// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Provides static storage for authentication details during app runtime.

using System;

namespace LockIt.Services
{
    /// <summary>
    /// A static service for storing authentication-related data such as Firebase ID token and email.
    /// Used across the application for accessing user context.
    /// </summary>
    public static class AuthService
    {
        /// <summary>
        /// Gets or sets the Firebase ID token for the currently authenticated user.
        /// </summary>
        public static string IdToken { get; set; }

        /// <summary>
        /// Gets or sets the email of the currently authenticated user.
        /// </summary>
        public static string Email { get; set; }
    }
}
