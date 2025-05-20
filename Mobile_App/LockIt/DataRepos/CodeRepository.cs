using LockIt.Services;
using LockIt.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.Repos
{
    public class CodeRepository
    {
        private readonly string _dbUrl;
        private readonly string _idToken;
        private readonly HttpClient _client;

        public CodeRepository(string dbUrl, string idToken = null)
        {
            _dbUrl = dbUrl.TrimEnd('/');
            _idToken = idToken;
            _client = new HttpClient();
        }

        public async Task<string> GetCodeAsync()
        {
            if (string.IsNullOrEmpty(AuthService.Email))
                throw new InvalidOperationException("No email available in AuthService.");

            return await GetCodeAsync(AuthService.Email);
        }

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

        public async Task<bool> SetCodeAsync(string code)
        {
            var safeKey = AuthService.Email.Replace(".", "_").Replace("@", "_"); ;
            var url = $"{_dbUrl}/codes/{safeKey}.json?auth={_idToken}";
            var content = new StringContent(JsonSerializer.Serialize(code), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCodeAsync()
        {
            var safeKey = AuthService.Email.Replace(".", "_");
            var url = $"{_dbUrl}/codes/{safeKey}.json?auth={_idToken}";
            var response = await _client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

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
