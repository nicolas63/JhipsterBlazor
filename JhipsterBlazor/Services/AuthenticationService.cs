using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string AuthenticatationUrl = "/api/authenticate";
        private const string AccountUrl = "/api/account";
        private const string AuthorizationHeader = "Authorization";
        private const string JhiAuthenticationtoken = "jhi-authenticationtoken";

        private readonly HttpClient _httpClient;
        private readonly ISyncSessionStorageService _sessionStorage;

        public bool IsAuthenticated { get; set; }
        public UserModel CurrentUser { get; set; }
        
        public AuthenticationService(HttpClient httpClient, ISyncSessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
            var token = _sessionStorage.GetItem<string>(JhiAuthenticationtoken);
            if (!string.IsNullOrEmpty(token))
            {
                SetUserAndAuthorizationHeader(new JwtToken(){IdToken = token}); 
            }
        }

        public async Task<bool> SignIn(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(AuthenticatationUrl, loginModel);
            if (result.IsSuccessStatusCode)
            {
                var bearer = await result.Content.ReadFromJsonAsync<JwtToken>();
                _sessionStorage.SetItem(JhiAuthenticationtoken, bearer.IdToken); 
                await SetUserAndAuthorizationHeader(bearer);
            }
            return IsAuthenticated;
        }

        public async Task SignOut()
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            IsAuthenticated = false;
            CurrentUser = null;
            _sessionStorage.RemoveItem(JhiAuthenticationtoken);
        }

        private async Task SetUserAndAuthorizationHeader(JwtToken jwtToken)
        {
            IsAuthenticated = true;
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {jwtToken.IdToken}");
            try
            {
                CurrentUser = await _httpClient.GetFromJsonAsync<UserModel>(AccountUrl);
            }
            catch
            {
                IsAuthenticated = false;
            }
        }

      
    }
}
