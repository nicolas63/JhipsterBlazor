using System.Collections.Generic;
using JhipsterBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Entities
{
    public partial class Country : ComponentBase
    {
        public IList<CountryModel> Countries { get; set; } = new List<CountryModel>();

        public Country()
        {
            Countries.Add(new CountryModel()
            {
                Region = new RegionModel()
                {
                    Id = 5
                },
                CountryName = "CountryName",
                Id = 10
            });
        }

    }
}
