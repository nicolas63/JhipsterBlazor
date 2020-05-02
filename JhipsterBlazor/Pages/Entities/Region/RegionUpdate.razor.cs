using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities.Region
{
    public partial class RegionUpdate
    {
        [Parameter]
        public int Id { get; set; }
        
        [Inject] 
        public IRegionService RegionService { get; set; }
        
        public RegionModel RegionModel { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                RegionModel = await RegionService.Get(Id.ToString());
            }
            else
            {
                RegionModel = new RegionModel();
            }
        }

        private async Task HandleSubmit()
        {
            //var result = await (AuthenticationService as IAuthenticationService).SignIn(LoginModel);
            //IsAuthenticateError = !result;
            //LoginModel = new LoginModel();
            //if (result)
            //{
            //    NavigationManager.NavigateTo("/");
            //}
        }
    }
}
