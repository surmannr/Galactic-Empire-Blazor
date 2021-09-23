using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticEmpire.Api.ExtensionsAndServices.Identity
{
    public class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("GalacticEmpireApi.all"),
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("GalacticEmpireApi")
            {
                Scopes = new List<string>{ "GalacticEmpireApi.all" },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };

        public static IEnumerable<IdentityServer4.Models.Client> Clients => new IdentityServer4.Models.Client[]
        {
            new IdentityServer4.Models.Client
            {
                ClientId = "GalacticEmpire.Client",
                ClientName = "Postman",
                AllowedGrantTypes = new List<string>
                {
                    GrantType.ResourceOwnerPassword,
                    GrantType.AuthorizationCode
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowAccessTokensViaBrowser =true,
                Enabled = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "GalacticEmpireApi.all"
                },
                RedirectUris =
                {
                    "http://localhost:44331"
                },
                PostLogoutRedirectUris = 
                {
                    "http://localhost:44331"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:44331",
                    "http://localhost:5000",
                    "https://localhost:5001"
                }
            },
            new IdentityServer4.Models.Client
            {
                ClientId = "blazorWASM",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                Enabled = true,
                AllowAccessTokensViaBrowser =true,
                AllowedCorsOrigins =
                { 
                    "https://localhost:5001",
                    "https://localhost:5000",
                    "https://localhost:44331"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "GalacticEmpireApi.all"
                },
                RedirectUris = { "https://localhost:44331/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:44331/authentication/logout-callback" }
            },
        };
    }
}
