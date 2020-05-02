using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices.Country;
using JhipsterBlazor.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities.Country
{
    public partial class CountryUpdate
    {
        [Parameter]
        public int Id { get; set; }
        
        [Inject] 
        public ICountryService CountryService { get; set; }

        [Inject]
        public IRegionService RegionService { get; set; }

        public CountryModel CountryModel { get; set; } = new CountryModel();

        public IEnumerable<long> Regions { get; set; } = new List<long>();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                CountryModel = await CountryService.Get(Id.ToString());
            }
            Regions = (await RegionService.GetAll()).Select(region => region.Id);
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
