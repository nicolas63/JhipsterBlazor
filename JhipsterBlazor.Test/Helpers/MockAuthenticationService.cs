using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Test.Helpers
{
    public class MockAuthenticationService : AuthenticationStateProvider,IAuthenticationService
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public UserModel CurrentUser
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public JwtToken JwtToken
        {
            get => throw new NotImplementedException();
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
