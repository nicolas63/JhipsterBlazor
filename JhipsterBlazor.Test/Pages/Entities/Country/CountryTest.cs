using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.EntityServices.Country;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterBlazor.Test.Pages.Entities.Country
{
    public class CountryTest : TestContext
    {
        private readonly Mock<ICountryService> _countryService;
        private readonly Mock<IModalService> _modalService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public CountryTest()
        {
            _countryService = new Mock<ICountryService>();
            _modalService = new Mock<IModalService>();
            Services.AddSingleton<ICountryService>(_countryService.Object);
            Services.AddSingleton<IModalService>(_modalService.Object);
            Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            Services.AddHttpClientInterceptor();
        }


        [Fact]
        public void Should_DisplayAllCountries_When_CountriesArePresent()
        {
            //Arrange
            var countries = _fixture.CreateMany<CountryModel>(10);
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));
            var userDetail = RenderComponent<JhipsterBlazor.Pages.Entities.Country.Country>();


            // Act
            var countriesTableBody = userDetail.Find("tbody");

            // Assert
            countriesTableBody.ChildElementCount.Should().Be(10);
        }

        [Fact]
        public void Should_DisplayNoCountry_When_CountriesLengthIsZero()
        {
            //Arrange
            var countries = new List<CountryModel>();
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));
            var countryDetail = RenderComponent<JhipsterBlazor.Pages.Entities.Country.Country>();


            // Act
            var span = countryDetail.Find("div>span");

            // Assert
            span.MarkupMatches("<span>No countries found</span>");
        }

    }
}
