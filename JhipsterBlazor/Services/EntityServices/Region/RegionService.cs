using System.Net.Http;
using JhipsterBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterBlazor.Services.EntityServices.Region
{
    public class RegionService : AbstractEntityService<RegionModel>,IRegionService
    {
        public RegionService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,IAlertService alertService) 
            : base(httpClient, authenticationStateProvider, alertService, "/api/regions")
        {
        }
    }
}
