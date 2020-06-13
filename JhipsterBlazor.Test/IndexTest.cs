using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Bunit;
using JhipsterBlazor.Models;
using JhipsterBlazor.Pages;
using JhipsterBlazor.Test.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using NullLogger = Microsoft.Extensions.Logging.Abstractions.NullLogger;

namespace JhipsterBlazor.Test
{
    public class IndexTest : TestContext
    {
        [Fact]
        public void Should_CallSignInMethod_When_SignInWasClick()
        {
            //Arrange
            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);
            Services.AddMockUnAuthenticateAuthorization();
            var authenticationStateProvider = Services.GetService<AuthenticationStateProvider>();

            var index = RenderComponent<Index>(ComponentParameterFactory.CascadingValue(authenticationStateProvider.GetAuthenticationStateAsync()));
           
            // Act
            index.Find(".alert-link").Click();

            // Assert
            modalService.Verify(mock => mock.Show<Login>(It.IsAny<string>()),
                Times.Once());
        }
    }
}
