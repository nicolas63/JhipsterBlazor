using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.EntityServices
{
    public interface ICountryService
    {
        public Task<IList<CountryModel>> GetAll();
    }
}
