using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.AccountServices;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService{ get; set; }

        private RegisterModel RegisterModel = new RegisterModel();


        protected override async Task OnInitializedAsync()
        {
        }

        private async Task HandleSubmit()
        {
            var result = await RegisterService.Save(new UserSaveModel{
                Email = RegisterModel.Email,
                Login = RegisterModel.Username,
                Password = RegisterModel.Password
            });
        }
    }
}
