using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.EntityServices.Region
{
    public interface IRegionService
    {
        public Task<IList<RegionModel>> GetAll();

        public Task<RegionModel> Get(string id);

        public Task Add(RegionModel model);

        Task Update(RegionModel model);

        Task Delete(string id);
    }
}
