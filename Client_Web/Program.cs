using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services
    .AddAuthentication(options =>
     {
         options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = MyIdentityServerConstants.MyAuthenticationScheme;

     })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(MyIdentityServerConstants.MyAuthenticationScheme, options =>
    {
        //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = "https://localhost:7001";

        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        // ...
        options.Scope.Add(MyIdentityServerConstants.MyStandardScopes.PersianProfile);
        options.ClaimActions.MapJsonKey(MyJwtClaimTypes.Nationalcode, MyJwtClaimTypes.PersianBirthdate);
        // ...

        options.GetClaimsFromUserInfoEndpoint = true;

        options.MapInboundClaims = false;
        options.DisableTelemetry = true;

        options.SaveTokens = true;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
