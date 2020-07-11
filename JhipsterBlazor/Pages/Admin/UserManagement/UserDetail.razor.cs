using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Pages.Utils;
using JhipsterBlazor.Services.EntityServices.User;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Admin.UserManagement
{
    public partial class UserDetail
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        private UserModel CurrentUser { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            CurrentUser = null;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                CurrentUser = await UserService.Get(Id); 
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
