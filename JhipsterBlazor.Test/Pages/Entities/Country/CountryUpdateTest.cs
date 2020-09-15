using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterBlazor.Models;
using JhipsterBlazor.Pages.Utils;
using JhipsterBlazor.Services.EntityServices.Country;
using JhipsterBlazor.Services.EntityServices.Region;
using JhipsterBlazor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterBlazor.Test.Pages.Entities.Country
{
    public class CountryUpdateTest : TestContext
    {
        private readonly Mock<ICountryService> _countryService;
        private readonly Mock<IRegionService> _regionService;
        private readonly Mock<IModalService> _modalService;
        private readonly Mock<INavigationService> _navidationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public CountryUpdateTest()
        {
            _countryService = new Mock<ICountryService>();
            _regionService = new Mock<IRegionService>();
            _modalService = new Mock<IModalService>();
            _navidationService = new Mock<INavigationService>();
            Services.AddSingleton<INavigationService>(_navidationService.Object);
            Services.AddSingleton<ICountryService>(_countryService.Object);
            Services.AddSingleton<IRegionService>(_regionService.Object);
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
        public void Should_UpdateSelectedCountry_When_FormIsSubmitWithId()
        {
            //Arrange
            var regions = _fixture.CreateMany<RegionModel>(10);
            _regionService.Setup(service => service.GetAll()).Returns(Task.FromResult(regions.ToList() as IList<RegionModel>));

            var countryToUpdate = _fixture.Create<CountryModel>();
            countryToUpdate.Id = 1; 

            _countryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(countryToUpdate));
            
            var updateCountryPage = RenderComponent<JhipsterBlazor.Pages.Entities.Country.CountryUpdate>(ComponentParameter.CreateParameter("Id", 1));


            // Act
            var countryform = updateCountryPage.Find("form");
            countryform.Submit();

            // Assert
            _countryService.Verify(service => service.Update(countryToUpdate),Times.Once);
        }

        [Fact]
        public void Should_AddCountry_When_FormIsSubmitWithoutId()
        {
            //Arrange
            var regions = _fixture.CreateMany<RegionModel>(10);
            _regionService.Setup(service => service.GetAll()).Returns(Task.FromResult(regions.ToList() as IList<RegionModel>));
            
            var updateCountryPage = RenderComponent<JhipsterBlazor.Pages.Entities.Country.CountryUpdate>(ComponentParameter.CreateParameter("Id", 0));


            // Act
            var countryform = updateCountryPage.Find("form");
            countryform.Submit();

            // Assert
            _countryService.Verify(service => service.Add(It.IsAny<CountryModel>()), Times.Once);
        }

    }
}
