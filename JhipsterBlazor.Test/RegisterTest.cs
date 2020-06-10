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
            Services.AddSingleton<IRegisterService ,MockRegisterService>();

            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);

            var register = RenderComponent<Register>();

            register.Instance.RegisterModel.Username = "testSuccess";
            register.Instance.RegisterModel.Email = "testSuccess@testSuccess.testSuccess";
            register.Instance.RegisterModel.Password = "testSuccess";
            register.Instance.RegisterModel.ConfirmPassword = "testSuccess";


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
            Services.AddSingleton<IRegisterService, MockRegisterService>();

            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);

            var register = RenderComponent<Register>();

            register.Instance.RegisterModel.Username = "test";
            register.Instance.RegisterModel.Email = "test@test.tests";
            register.Instance.RegisterModel.Password = "testtest";
            register.Instance.RegisterModel.ConfirmPassword = "testtest";


            // Act
            var test = register.Find("form");
            test.Submit();

            Thread.Sleep(1000); //wait register handleSubmit

            // Assert
            register.Instance.Success.Should().BeFalse();
            register.Find("div").Children.Children("div").Children("div").Children("div").First().MarkupMatches(
                @"<div class=""alert alert-danger"">
                            <strong>Registration failed!</strong> Please try again later.
                        </div>");
        }
    }
}