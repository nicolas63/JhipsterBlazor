using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities.Region
{
    public partial class Region : ComponentBase
    {
        [Inject]
        private IRegionService RegionService { get; set; }

        private IList<RegionModel> Regions { get; set; } = new List<RegionModel>();

        
        protected override async Task OnInitializedAsync()
        {
            Regions = await RegionService.GetAll();
        }
    }
}
