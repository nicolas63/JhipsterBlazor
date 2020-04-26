using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string AuthenticatationUrl = "/api/authenticate";
        private const string AccountUrl = "/api/account";

        private readonly HttpClient _httpClient;
        public bool IsAuthenticated { get; set; }
        public UserModel CurrentUser { get; set; }

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<bool> Authenticate(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(AuthenticatationUrl, loginModel);
            if (result.IsSuccessStatusCode)
            {
                IsAuthenticated = true;
                var bearer = await result.Content.ReadFromJsonAsync<JwtToken>();
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {bearer.IdToken}");
                CurrentUser = await _httpClient.GetFromJsonAsync<UserModel>(AccountUrl); 
            }
            return IsAuthenticated; 
        }
    }
}
