using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Services.EntityServices.User
{
    public class UserService : AbstractEntityService<UserModel>,IUserService
    {
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider, "/api/users")
        {
        }
    }
}
