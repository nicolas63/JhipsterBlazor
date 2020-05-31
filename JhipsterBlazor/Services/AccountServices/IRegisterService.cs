using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.AccountServices
{
    interface IRegisterService
    {
        Task<bool> Save(UserSaveModel registerModel);
    }
}
