using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.AccountServices
{
    public interface IRegisterService
    {
        Task<HttpResponseMessage> Save(UserSaveModel registerModel);
    }
}
