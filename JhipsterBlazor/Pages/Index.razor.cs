using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public UserModel CurrentUser => AuthenticationService.CurrentUser;

        public EventCallback<UserModel> CurrentUserChanged { get; set; }

        
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            OnCurrentUserChanged();
            return base.OnAfterRenderAsync(firstRender);
        }
        
        private void OnCurrentUserChanged()
        {
            CurrentUserChanged.InvokeAsync(CurrentUser);
        }


    }
}
