using DoctorFit.IDP.Data;
using Duende.IdentityServer.Models;

namespace IDP
{
    public static class Config
    {
        public static IEnumerable<Client> Clients => new[]
        {
        new Client
        {
            ClientId = "doctorfit_blazor_client",
            AllowedGrantTypes = GrantTypes.Code,
            RequireClientSecret = false,
            RedirectUris = { "https://localhost:5003/authentication/login-callback" },
            PostLogoutRedirectUris = { "https://localhost:5003/" },
            AllowedScopes = { "openid", "profile", "roles", "doctorfit_api","offline_access" },//This defines what the client app is allowed to access via tokens.
            AllowOfflineAccess = true,
            AllowedCorsOrigins = { "https://localhost:5003" },
            RequirePkce = true,
            AllowAccessTokensViaBrowser = true
        }
    };

        public static IEnumerable<ApiScope> ApiScopes => new[]//به چه پروژه ای پی آی دسترسی داشته باشه
        {
        new ApiScope("doctorfit_api", "به سرویس دکتر فیت خوش آمدید")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]//Specifies what identity-related data a client can receive in the ID token.
        {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource("roles", "User Roles", new[] { "role" })
        };
    }

}
