using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities
{
    public partial class Country : ComponentBase
    {
        [Inject]
        private ICountryService CountryService { get; set; }

        private IList<CountryModel> Countries { get; set; } = new List<CountryModel>();
        
        protected override async Task OnInitializedAsync()
        {
            Countries = await CountryService.GetAll();
        }
    }
}
