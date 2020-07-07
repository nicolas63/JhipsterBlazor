using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using FluentAssertions;
using JhipsterBlazor.Services;
using JhipsterBlazor.Test.TestPages;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SharedModel.Models;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterBlazor.Test
{
    public class AlertErrorTest : TestContext
    {
        [Fact]
        public async void Should_Display404_When_404()
        {
            Services.AddHttpClientInterceptor();
            var alertComponent = RenderComponent<TestAlertError>();
            var handlerMock = new Mock<HttpMessageHandler>();
            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(@""),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.EnableIntercept(Services);
            try
            {
                await httpClient.GetAsync(Configuration.BaseUri);
            }
            catch (Exception)
            {
                // ignored
            }

            await alertComponent.InvokeAsync(() => alertComponent.Instance.AlertError.AddAlert(new JhiAlert()));
            
            alertComponent.WaitForState(() => 
            {
                return alertComponent.Find("div").Children.Length > 0;
                
            }, TimeSpan.FromSeconds(30));
            
            
        }

    }
}
