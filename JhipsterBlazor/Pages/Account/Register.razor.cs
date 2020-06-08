using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.AccountServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using Blazorise;
using SharedModel.Constants;

namespace JhipsterBlazor.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService{ get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        private RegisterModel RegisterModel = new RegisterModel();

        private EditForm editForm;

        private bool Success { get; set; }
        private bool Error { get; set; }
        private bool DoNotMatch { get; set; }
        private bool ErrorEmailExists { get; set; }
        private bool ErrorUserExists { get; set; }

        void Validate(ValidatorEventArgs e, string textError)
        {
            e.ErrorText = "superTest";
            if(e.Value != null)
            {
                e.Status = Convert.ToString(e.Value)?.Length >= 6 ? ValidationStatus.Success : ValidationStatus.Error;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
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
            if (result.StatusCode != HttpStatusCode.BadRequest) // other status code
            {
                Error = true;
                return;
            }

            try
            {
                var res = await result.Content.ReadFromJsonAsync<RegisterResultRequest>();
                ErrorEmailExists = res.Type == ErrorConst.EmailAlreadyUsedType;
                ErrorUserExists = res.Type == ErrorConst.LoginAlreadyUsedType;
                if (!ErrorEmailExists && !ErrorUserExists) // if unknown error
                {
                    Error = true;
                }
            }
            catch (Exception)
            {
                Error = true; // json pars error
            }
            
        }

        private async Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
        }
    }
}
