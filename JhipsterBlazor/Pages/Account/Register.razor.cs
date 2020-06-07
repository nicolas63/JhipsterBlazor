using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.AccountServices;
using JhipsterBlazor.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json.Linq;

namespace JhipsterBlazor.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService{ get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        private RegisterModel RegisterModel = new RegisterModel();

        private bool Success { get; set; }
        private bool Error { get; set; }
        private bool DoNotMatch { get; set; }
        private bool ErrorEmailExists { get; set; }
        private bool ErrorUserExists { get; set; }



        protected override async Task OnInitializedAsync()
        {
        }

        private async Task HandleSubmit()
        {
            SetAllErrorFalse();
            if (!RegisterModel.Password.Equals(RegisterModel.ConfirmPassword))
            {
                DoNotMatch = true;
                return;
            }
            var result = await RegisterService.Save(new UserSaveModel{
                Email = RegisterModel.Email,
                Login = RegisterModel.Username,
                Password = RegisterModel.Password,
                LangKey = "en"
            });
            if (result.IsSuccessStatusCode)
            {
                Success = true;
            }
            else
            {
                await ProcessError(result);
            }
        }

        private void SetAllErrorFalse()
        {
            Success = false;
            Error = false;
            ErrorEmailExists = false;
            ErrorUserExists = false;
            DoNotMatch = false;
        }

        private async Task ProcessError(HttpResponseMessage result)
        {
            JObject errorResultJson = JObject.Parse(await result.Content.ReadAsStringAsync());
            string typeString = errorResultJson.GetValue("type").Value<string>();
            if (result.StatusCode != HttpStatusCode.BadRequest || typeString == null) // json pars error or other status code
            {
                Error = true;
                return;
            }
            ErrorEmailExists = typeString == ErrorConst.EmailAlreadyUsedType;
            ErrorUserExists = typeString == ErrorConst.LoginAlreadyUsedType;
            if (!ErrorEmailExists && !ErrorUserExists) // if unknown error
            {
                Error = true;
            }
        }

        private async Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
        }
    }
}
