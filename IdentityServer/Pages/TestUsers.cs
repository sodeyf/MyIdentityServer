// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using Common.MyConstants;

namespace IdentityServer;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "Ferdows Blvd",
                locality = "Tehran",
                postal_code = "12121212",
                country = "Iran"
            };

            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "sodeyf",
                    Password = "1",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "mohammad"),
                        new Claim(JwtClaimTypes.GivenName, "javad"),
                        new Claim(JwtClaimTypes.FamilyName, "qeidarlo"),
                        new Claim(JwtClaimTypes.Email, "sodeyf@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://sodeyf.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json),

                        new Claim(MyJwtClaimTypes.Nationalcode, "0320227561"),
                        new Claim(MyJwtClaimTypes.PersianBirthdate, "1362/08/04"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "ali",
                    Password = "1",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "ali"),
                        new Claim(JwtClaimTypes.GivenName, "alireza"),
                        new Claim(JwtClaimTypes.FamilyName, "qeidarlo"),
                        new Claim(JwtClaimTypes.Email, "ali@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://ali.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json),

                        new Claim(MyJwtClaimTypes.Nationalcode, "0112355779"),
                        new Claim(MyJwtClaimTypes.PersianBirthdate, "1387/08/04"),
                    }
                }
            };
        }
    }
}