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
           // Arrange
           //var modalService = new Mock<IModalService>();
           // var authenticationService = new Mock<MockAuthenticationService>();
           // var authorizationPolicyProvider = new Mock<IAuthorizationPolicyProvider>();
           // var authorizationService = new Mock<IAuthorizationService>();
           // authenticationService.Setup(authenticationService =>
           //     authenticationService.SignIn(It.IsAny<LoginModel>()));
           // Services.AddSingleton<AuthenticationStateProvider>(authenticationService.Object);
           // Services.AddSingleton<IModalService>(modalService.Object);
           // //Services.AddSingleton(typeof(ILogger<>), typeof(NullLogger));
           // Services.AddScoped<IAuthorizationPolicyProvider, MockAuthorizationPolicyProvider>();
           // Services.AddScoped<IAuthorizationService, MockAuthorizationService>();
           // Services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();

           // var authorizeComponent = RenderComponent<CascadingAuthenticationState>();
           // // Act
           // index.Find("a").Click();

           // // Assert
           // modalService.Verify(mock => mock.Show<Login>(It.IsAny<string>()),
           //     Times.Once());
        }
    }
}
