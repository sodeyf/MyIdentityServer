using Common.MyConstants;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = MyIdentityServerConstants.MyStandardScopes.PersianProfile,
                UserClaims = new List<string>
                {
                    "natinalcode",
                    "persianbirthdate"
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "api1", displayName: "My API"),
                new ApiScope(name: "api2", displayName: "My API"),
                new ApiScope(name: "api3", displayName: "My API"),
                new ApiScope(name: "api4", displayName: "My API"),
                new ApiScope(name: "api5", displayName: "My API"),
                new ApiScope(name: "api6", displayName: "My API")
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                //--------------------------------------------------------------- machine to machine client (from quickstart 1)
                 new Client
                {
                    ClientId = "client1",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256()), new Secret("secret3".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1", "api2" }
                },
                new Client
                {
                    ClientId = "client2",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret2".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api3", "api4", "api5", "api6", "api7", "api8", "api9" }
                },
                //------------------------------------------------------------------------------------------
                // interactive ASP.NET Core Web App
                new Client
                {
                    ClientId = "web",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
            
                    // where to redirect to after login
                    RedirectUris = { $"{MyUrls.WebClient}/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { $"{MyUrls.WebClient}/signout-callback-oidc" },

                    // allow the web client to request a refresh token
                    AllowOfflineAccess = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        MyIdentityServerConstants.MyStandardScopes.PersianProfile,

                        "api1"
                    }
                }
            };
}