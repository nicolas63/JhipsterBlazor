using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhipsterBlazor.Shared.Constants
{
    public static class ErrorConst
    {
        public const string ProblemBaseUrl = "https://www.jhipster.tech/problem";
        public const string EmailAlreadyUsedType = ProblemBaseUrl + "/email-already-used";
        public const string LoginAlreadyUsedType = ProblemBaseUrl + "/login-already-used";
    }
}
