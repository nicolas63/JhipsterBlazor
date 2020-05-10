using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationService { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        private UserModel CurrentUser => (AuthenticationService as IAuthenticationService)?.CurrentUser;

        private async Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
        }

    }
}
