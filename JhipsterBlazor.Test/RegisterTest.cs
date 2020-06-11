using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Blazored.Modal;
using Blazored.Modal.Services;
using Xunit;
using Bunit;
using Bunit.Mocking.JSInterop;
using FluentAssertions;
using JhipsterBlazor.Models;
using JhipsterBlazor.Pages;
using JhipsterBlazor.Pages.Account;
using JhipsterBlazor.Services;
using JhipsterBlazor.Services.AccountServices;
using JhipsterBlazor.Test.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using static Bunit.ComponentParameterFactory;

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
            var register = InitTestRegister("testSuccess", "testSuccess@testSuccess.testSuccess", "testSuccess", "testSuccess");

            // Act
            var test = register.Find("form");
            test.Submit();

            Thread.Sleep(1000); //wait register handleSubmit

            // Assert
            register.Instance.Success.Should().BeTrue();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-success"">
                            <strong>Registration saved!</strong> Please check your email for confirmation.
                        </div>");
        }

        [Fact]
        public void Should_Error_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister("test", "test@test.tests", "testtest", "testtest");

            // Assert
            register.Instance.Success.Should().BeFalse();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-danger"">
                            <strong>Registration failed!</strong> Please try again later.
                        </div>");
        }

        [Fact]
        public void Should_ErrorEmail_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister("testEmail", "test@test.tests", "testtest", "testtest");

            // Assert
            register.Instance.Success.Should().BeFalse();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-danger"">
                            <strong>Email is already in use!</strong> Please choose another one.
                        </div>");
        }

        [Fact]
        public void Should_ErrorLogin_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister("testLogin", "test@test.tests", "testtest", "testtest");

            // Assert
            register.Instance.Success.Should().BeFalse();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-danger"">
                            <strong>Login name already registered!</strong> Please choose another one.
                        </div>");
        }

        [Fact]
        public void Should_ErrorPasswordNotMatch_When_RegisterFormIsSubmit()
        {
            // Arrange
            var register = InitTestRegister("testLogin", "test@test.tests", "testtest1", "testtest2");

            // Assert
            register.Instance.Success.Should().BeFalse();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-danger"">
                            The password and its confirmation do not match!
                        </div>");
        }

        private IRenderedComponent<Register> InitTestRegister(string username, string email, string password, string confirmPassword)
        {
            Services.AddSingleton<IRegisterService, MockRegisterService>();

            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);

            var register = RenderComponent<Register>();

            register.Instance.RegisterModel.Username = username;
            register.Instance.RegisterModel.Email = email;
            register.Instance.RegisterModel.Password = password;
            register.Instance.RegisterModel.ConfirmPassword = confirmPassword;


            // Act
            var test = register.Find("form");
            test.Submit();

            Thread.Sleep(1000); //wait register handleSubmit
            return register;
        }
    }
}