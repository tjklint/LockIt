// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Handles code and visitor permission operations with Firebase Realtime Database.

using LockIt.Services;
using LockIt.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.Repos
{
    /// <summary>
    /// Repository responsible for managing access codes and visitor permissions in Firebase.
    /// </summary>
    public class CodeRepository
    {
        private readonly string _dbUrl;
        private readonly string _idToken;
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeRepository"/> class.
        /// </summary>
        /// <param name="dbUrl">The base URL of the Firebase Realtime Database.</param>
        /// <param name="idToken">The authentication token for Firebase (optional).</param>
        public CodeRepository(string dbUrl, string idToken = null)
        {
            _dbUrl = dbUrl.TrimEnd('/');
            _idToken = idToken;
            _client = new HttpClient();
        }

        /// <summary>
        /// Retrieves the access code for the currently authenticated user.
        /// </summary>
        /// <returns>The stored access code as a string.</returns>
        /// <exception cref="InvalidOperationException">Thrown when AuthService does not contain an email.</exception>
        public async Task<string> GetCodeAsync()
        {
            if (string.IsNullOrEmpty(AuthService.Email))
                throw new InvalidOperationException("No email available in AuthService.");

            return await GetCodeAsync(AuthService.Email);
        }

        /// <summary>
        /// Retrieves the access code for the specified email.
        /// </summary>
        /// <param name="email">The email address used as the key to retrieve the code.</param>
        /// <returns>The stored access code as a string, or null if unsuccessful.</returns>
        public async Task<string> GetCodeAsync(string email)
        {
            var safeKey = email.Replace(".", "_").Replace("@", "_");
            var url = $"{_dbUrl}/codes/{safeKey}.json";

            if (!string.IsNullOrEmpty(_idToken))
                url += $"?auth={_idToken}";

            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Stores the access code for the currently authenticated user.
        /// </summary>
        /// <param name="code">The access code to store.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> SetCodeAsync(string code)
        {
            var safeKey = AuthService.Email.Replace(".", "_");
            var url = $"{_dbUrl}/codes/{safeKey}.json?auth={_idToken}";
            var content = new StringContent(JsonSerializer.Serialize(code), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes the access code for the currently authenticated user.
        /// </summary>
        /// <returns>True if the code was successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteCodeAsync()
        {
            var safeKey = AuthService.Email.Replace(".", "_");
            var url = $"{_dbUrl}/codes/{safeKey}.json?auth={_idToken}";
            var response = await _client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retrieves the visitor permissions for a given homeowner email.
        /// </summary>
        /// <param name="email">The homeowner's email address.</param>
        /// <returns>An instance of <see cref="VisitorPermissions"/> or null if retrieval fails.</returns>
        public async Task<VisitorPermissions> GetVisitorPermissionsAsync(string email)
        {
            var safeKey = email.Replace(".", "_").Replace("@", "_");
            var url = $"{_dbUrl}/visitor_permissions/{safeKey}.json";
            if (!string.IsNullOrEmpty(_idToken)) url += $"?auth={_idToken}";

            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VisitorPermissions>(json);
        }

        /// <summary>
        /// Stores visitor permissions for a specific homeowner.
        /// </summary>
        /// <param name="email">The homeowner's email address.</param>
        /// <param name="perms">The <see cref="VisitorPermissions"/> to store.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public async Task<bool> SetVisitorPermissionsAsync(string email, VisitorPermissions perms)
        {
            var safeKey = email.Replace(".", "_").Replace("@", "_");
            var url = $"{_dbUrl}/visitor_permissions/{safeKey}.json?auth={_idToken}";
            var json = JsonSerializer.Serialize(perms);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }
}
