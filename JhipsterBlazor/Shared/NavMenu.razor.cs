using System.Threading.Tasks;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public async Task SignOut()
        {
            await (AuthenticationService as IAuthenticationService).SignOut(); 
            NavigationManager.NavigateTo("/");
        }
    }
}
