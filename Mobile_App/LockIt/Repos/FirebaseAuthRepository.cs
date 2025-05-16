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
            apiKey = "";
        }

        public async Task<FirebaseAuthResponse> LoginAsync(string email, string password)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
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
}

