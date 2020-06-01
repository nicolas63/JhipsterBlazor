using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using JhipsterBlazor.Services.EntityServices.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Pages.Admin.UserManagement
{
    public partial class UserManagement
    {
        private IList<UserModel> UserModels { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private IAuthenticationService AuthenticationService => AuthenticationStateProvider as IAuthenticationService; 

        protected override async Task OnInitializedAsync()
        {
            UserModels = await UserService.GetAll();
        }

        public async Task CreateUser()
        {

        }

        private async Task ActiveUser(UserModel user, bool activate)
        {

        }

        private async Task DeleteUser(UserModel user)
        {

        }
    }
}
