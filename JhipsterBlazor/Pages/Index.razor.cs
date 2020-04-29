using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationService { get; set; }
        
        public UserModel CurrentUser => (AuthenticationService as IAuthenticationService)?.CurrentUser;
        
    }
}
