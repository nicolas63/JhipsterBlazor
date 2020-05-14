using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.EntityServices.Country
{
    public interface ICountryService
    {
        Task<IList<CountryModel>> GetAll();

        Task<CountryModel> Get(string id);

        Task Add(CountryModel model);

        Task Update(CountryModel model);

        Task Delete(string id);
    }
}
