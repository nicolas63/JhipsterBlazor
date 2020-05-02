using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Pages.Utils;
using JhipsterBlazor.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities.Region
{
    public partial class RegionDetail
    {
        [Parameter]
        public int Id { get; set; }

        [Inject] 
        public IRegionService RegionService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public RegionModel Region { get; set; } = new RegionModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                Region = await RegionService.Get(Id.ToString()); 
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
