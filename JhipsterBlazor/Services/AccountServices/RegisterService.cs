using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterBlazor.Models;

namespace JhipsterBlazor.Services.AccountServices
{
    public class RegisterService : IRegisterService
    {
        private const string RegisterUrl = "/api/register";

        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<bool> Save(UserSaveModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync(RegisterUrl, registerModel);
            return result.IsSuccessStatusCode;
        }
    }
}
