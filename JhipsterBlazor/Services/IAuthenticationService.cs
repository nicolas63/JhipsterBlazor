using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services
{
    public interface IAuthenticationService
    {
        public bool IsAuthenticated { get; set; }

        Task<bool> Authenticate(LoginModel loginModel);

        public UserModel CurrentUser { get; set; }
    }
}