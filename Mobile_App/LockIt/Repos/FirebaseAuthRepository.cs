// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Handles Firebase authentication logic, including login and registration.

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LockIt.Models;
using Microsoft.Extensions.Configuration;
using LockIt.Helpers;

namespace LockIt.Repos
{
    /// <summary>
    /// Provides methods for interacting with Firebase Authentication API.
    /// </summary>
    public class FirebaseAuthRepository
    {
        private readonly string apiKey;
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="FirebaseAuthRepository"/> and loads API key from embedded appsettings.
        /// </summary>
        public FirebaseAuthRepository()
        {
            _client = new HttpClient();

            var root = AppSettingsLoader.Load();
            this.apiKey = root.GetProperty("Firebase").GetProperty("ApiKey").GetString();
        }

        /// <summary>
        /// Logs in a user with their email and password using Firebase Authentication.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="FirebaseAuthResponse"/> containing ID token and other auth info.</returns>
        /// <exception cref="FirebaseAuthException">Thrown if authentication fails or Firebase returns an error.</exception>
        public async Task<FirebaseAuthResponse> LoginAsync(string email, string password)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Deserialize<FirebaseErrorResponse>(json);
                throw new FirebaseAuthException(error?.Error?.Message ?? "Unknown error");
            }

            return JsonSerializer.Deserialize<FirebaseAuthResponse>(json);
        }

        /// <summary>
        /// Registers a new user in Firebase using the provided email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="FirebaseAuthResponse"/> with the registered user's authentication details.</returns>
        /// <exception cref="FirebaseAuthException">Thrown if registration fails or Firebase returns an error.</exception>
        public async Task<FirebaseAuthResponse> RegisterAsync(string email, string password)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<FirebaseAuthResponse>(jsonResponse);
        }
    }

    /// <summary>
    /// Represents a Firebase error response wrapper.
    /// </summary>
    public class FirebaseErrorResponse
    {
        /// <summary>
        /// The error details returned by Firebase.
        /// </summary>
        public FirebaseError Error { get; set; }
    }

    /// <summary>
    /// Represents a Firebase authentication error message.
    /// </summary>
    public class FirebaseError
    {
        /// <summary>
        /// The human-readable error message.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Represents an exception specific to Firebase authentication failures.
    /// </summary>
    public class FirebaseAuthException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FirebaseAuthException"/> with a specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public FirebaseAuthException(string message) : base(message) { }
    }
}
