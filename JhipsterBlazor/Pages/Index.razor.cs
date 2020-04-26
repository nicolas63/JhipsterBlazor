using JhipsterBlazor.Services;
using JhipsterBlazor.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

    }
}
