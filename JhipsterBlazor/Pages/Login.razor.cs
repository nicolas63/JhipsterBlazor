using System.Reflection;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public LoginModel LoginModel { get; set; } = new LoginModel();

        public bool IsAuthenticateError { get; set; }

        private async Task HandleSubmit()
        {
            IsAuthenticateError = !await AuthenticationService.Authenticate(LoginModel); 
        }



    }
}
