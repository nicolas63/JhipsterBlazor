using System.Linq;
using AngleSharp.Dom;
using Blazored.Modal.Services;
using Xunit;
using Bunit;
using FluentAssertions;
using JhipsterBlazor.Pages.Account;
using JhipsterBlazor.Services.AccountServices;
using JhipsterBlazor.Test.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace JhipsterBlazor.Test
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.egilhansen.com/docs/
    /// </summary>
    public class RegisterTest : TestContext
    {
        [Fact]
        public void Should_CallSaveInMethod_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister(MockRegisterService.SuccessUsername, "test@test.tests", "testtest", "testtest");

            // Assert
            register.WaitForAssertion(() => //wait register handleSubmit
            {
                register.Instance.Success.Should().BeTrue();
                register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                    @"<div class=""alert alert-success"">
                            <strong>Registration saved!</strong> Please check your email for confirmation.
                        </div>");
            });
            
        }

        [Fact]
        public void Should_Error_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister("test", "test@test.tests", "testtest", "testtest");

            // Assert
            register.WaitForAssertion(() => //wait register handleSubmit
            {
                register.Instance.Success.Should().BeFalse();
                register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                    @"<div class=""alert alert-danger"">
                            <strong>Registration failed!</strong> Please try again later.
                        </div>");
            });
        }

        [Fact]
        public void Should_ErrorEmail_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister(MockRegisterService.EmailUsername, "test@test.tests", "testtest", "testtest");

            // Assert
            register.WaitForAssertion(() => //wait register handleSubmit
            {
                register.Instance.Success.Should().BeFalse();
                register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                    @"<div class=""alert alert-danger"">
                            <strong>Email is already in use!</strong> Please choose another one.
                        </div>");
            });
        }

        [Fact]
        public void Should_ErrorLogin_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister(MockRegisterService.LoginUsername, "test@test.tests", "testtest", "testtest");

            // Assert
            register.WaitForAssertion(() => //wait register handleSubmit
            {
                register.Instance.Success.Should().BeFalse();
                register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                    @"<div class=""alert alert-danger"">
                            <strong>Login name already registered!</strong> Please choose another one.
                        </div>");
            });
        }

        private IRenderedComponent<Register> InitTestRegister(string username, string email, string password, string confirmPassword)
        {
            // Arrange
            Services.AddSingleton<IRegisterService, MockRegisterService>();

            var modalService = new Mock<IModalService>();
            Services.AddSingleton(modalService.Object);

            var register = RenderComponent<Register>();

            register.Instance.RegisterModel.Username = username;
            register.Instance.RegisterModel.Email = email;
            register.Instance.RegisterModel.Password = password;
            register.Instance.RegisterModel.ConfirmPassword = confirmPassword;

            // Act
            var form = register.Find("form");
            form.Submit();

            return register;
        }
    }
}