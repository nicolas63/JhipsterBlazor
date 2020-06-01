using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.EntityServices.User
{
    public interface IUserService
    {
        public Task<IList<UserModel>> GetAll();

        public Task<UserModel> Get(string id);

        public Task Add(UserModel model);

        Task Update(UserModel model);
    }
}
