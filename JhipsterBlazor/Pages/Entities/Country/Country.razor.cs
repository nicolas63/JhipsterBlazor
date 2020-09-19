using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices.Country;
using JhipsterBlazor.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities.Country
{
    public partial class Country : ComponentBase
    {
        [Inject]
        private ICountryService CountryService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<CountryModel> Countries { get; set; } = new List<CountryModel>();

        
        protected override async Task OnInitializedAsync()
        {
            Countries = await CountryService.GetAll();
        }

        private async Task Delete(long countryId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await CountryService.Delete(countryId.ToString());
                Countries.Remove(Countries.First(country => country.Id.Equals(countryId))); 
            }
        }
    }
}
