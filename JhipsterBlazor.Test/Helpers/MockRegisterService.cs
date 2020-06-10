using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JhipsterBlazor.Models;
using JhipsterBlazor.Services.AccountServices;
using SharedModel.Constants;

namespace JhipsterBlazor.Test.Helpers
{
    public class MockRegisterService : IRegisterService
    {
        public virtual Task<HttpResponseMessage> Save(UserSaveModel registerModel)
        {
            if (registerModel.Login == "testSuccess")
            {
                return Task.Run(() => new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("")
                });
            }

            /*if (registerModel.Login == "testEmail")
            {
                return Task.Run(() => new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ErrorConst.EmailAlreadyUsedType)
                });
            }

            if (registerModel.Login == "testLogin")
            {
                return Task.Run(() => new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ErrorConst.LoginAlreadyUsedType)
                });
            }*/
            return Task.Run(() => new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("")
            });


        }
    }
}
