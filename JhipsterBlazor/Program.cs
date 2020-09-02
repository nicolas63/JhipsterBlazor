using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using JhipsterBlazor.Pages.Utils;
using JhipsterBlazor.Services;
using JhipsterBlazor.Services.AccountServices;
using JhipsterBlazor.Services.EntityServices.Country;
using JhipsterBlazor.Services.EntityServices.Region;
using JhipsterBlazor.Services.EntityServices.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace JhipsterBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();


            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<ISessionStorageService, SessionStorageService>().AddSingleton<ISyncSessionStorageService, SessionStorageService>();
            builder.Services.AddBlazoredModal();

            builder.Services.AddSingleton<AuthenticationStateProvider, AuthenticationService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            
            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddSingleton<ICountryService, CountryService>();
            builder.Services.AddSingleton<IRegionService, RegionService>();
            builder.Services.AddSingleton<IRegisterService, RegisterService>();

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            }.EnableIntercept(sp));

            builder.Services.AddAuthorizationCore();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
