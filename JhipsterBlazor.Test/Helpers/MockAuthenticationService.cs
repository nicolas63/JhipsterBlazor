using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Test.Helpers
{
    public class MockAuthenticationService : AuthenticationStateProvider,IAuthenticationService
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new List<Claim>(), "JWT Auth");
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public bool IsAuthenticated
        {
            get => true;
            set => throw new NotImplementedException();
        }

        public UserModel CurrentUser
        {
            get => new UserModel();
            set => throw new NotImplementedException();
        }

        public JwtToken JwtToken
        {
            get => new JwtToken();
            set => throw new NotImplementedException();
        }

        public virtual async Task<bool> SignIn(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public virtual async Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
