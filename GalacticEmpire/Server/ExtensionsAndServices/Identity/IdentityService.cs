using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.ExtensionsAndServices.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpContext httpContext;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public string GetCurrentUserId()
        {
            return httpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        }
    }
}
