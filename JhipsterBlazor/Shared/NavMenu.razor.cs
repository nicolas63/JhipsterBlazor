using System.Threading.Tasks;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public async Task SignOut()
        {
            await AuthenticationService.SignOut(); 
            NavigationManager.NavigateTo("/");
        }
    }
}
