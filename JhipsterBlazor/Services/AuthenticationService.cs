using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using JhipsterBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Services
{
    public class AuthenticationService : AuthenticationStateProvider,IAuthenticationService
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
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
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
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (IsAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,CurrentUser.Login), 
                    new Claim(ClaimTypes.Name, CurrentUser.FirstName),
                    new Claim(ClaimTypes.Email,CurrentUser.Email),
                    new Claim(ClaimTypes.GivenName,CurrentUser.FirstName),
                    new Claim(ClaimTypes.Surname,CurrentUser.LastName),
                    new Claim("langKey",CurrentUser.LangKey),
                    new Claim("picture",CurrentUser.ImageUrl)
                };
                claims.AddRange(CurrentUser.Roles?.Select(role => new Claim(ClaimTypes.Role,role)) ?? Array.Empty<Claim>());
                identity = new ClaimsIdentity(claims);
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
