using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Services.EntityServices.User
{
    public class UserService : AbstractEntityService<UserModel>,IUserService
    {
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IAlertService alertService) : base(httpClient, authenticationStateProvider, alertService, "/api/users")
        {
        }

        public async Task<IEnumerable<string>> GetAllAuthorities()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{BaseUrl}/authorities");
        }
    }
}
