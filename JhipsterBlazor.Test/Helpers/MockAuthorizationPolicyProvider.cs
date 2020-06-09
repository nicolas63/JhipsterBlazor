using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JhipsterBlazor.Test.Helpers
{
    public class MockAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            throw new NotImplementedException();
        }
    }
}
