using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string AuthenticatationUri = "/api/authenticate";
        private readonly HttpClient _httpClient;
        public bool IsAuthenticated { get; set; }

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<bool> Authenticate(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(AuthenticatationUri,loginModel);
            if (result.IsSuccessStatusCode)
            {
                IsAuthenticated = true;
            }
            return IsAuthenticated; 
        }
    }
}
