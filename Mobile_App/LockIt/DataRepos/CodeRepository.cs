using LockIt.Services;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.Repos
{
    public class CodeRepository
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _authToken;

        public CodeRepository(string baseUrl, string authToken)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _authToken = authToken;
        }

        private string EmailKey => (AuthService.Email ?? throw new InvalidOperationException("No email set"))
                          .Replace(".", "_")
                          .Replace("@", "_");

        public async Task<string> GetCodeAsync()
        {
            var url = $"{_baseUrl}/codes/{EmailKey}.json?auth={_authToken}";
            var resp = await _client.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            var json = await resp.Content.ReadAsStringAsync();
            // stored as plain string in JSON
            return JsonSerializer.Deserialize<string>(json);
        }

        public async Task<bool> SetCodeAsync(string code)
        {
            var url = $"{_baseUrl}/codes/{EmailKey}.json?auth={_authToken}";
            var payload = new StringContent(JsonSerializer.Serialize(code),
                                           Encoding.UTF8, "application/json");
            var resp = await _client.PutAsync(url, payload);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCodeAsync()
        {
            var url = $"{_baseUrl}/codes/{EmailKey}.json?auth={_authToken}";
            var resp = await _client.DeleteAsync(url);
            return resp.IsSuccessStatusCode;
        }
    }
}
