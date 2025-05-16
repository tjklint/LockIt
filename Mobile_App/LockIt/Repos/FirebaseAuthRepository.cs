using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LockIt.Models;

namespace LockIt.Repos
{
    // TODO: ADD XML DOCUMENTATION WHEN CONFIRMATION IS GIVEN TO HANDLE FIREBASE IN A REPO OR SERVICE
    public class FirebaseAuthRepository
    {
        private readonly string apiKey;

        private readonly HttpClient _client;

        public FirebaseAuthRepository()
        {
            _client = new HttpClient();
            apiKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY") ?? throw new InvalidOperationException("API key not set");
        }

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


        public async Task<FirebaseAuthResponse> RegisterAsync(string email, string password)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
            var payload = new
            {
                email = email,
                password = password,
                returnSecureToken = true
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // TODO: Error Handling
            var result = JsonSerializer.Deserialize<FirebaseAuthResponse>(jsonResponse);
            return result;
        }
    }

    public class FirebaseErrorResponse
    {
        public FirebaseError Error { get; set; }
    }

    public class FirebaseError
    {
        public string Message { get; set; }
    }

    public class FirebaseAuthException : Exception
    {
        public FirebaseAuthException(string message) : base(message) { }
    }

}

