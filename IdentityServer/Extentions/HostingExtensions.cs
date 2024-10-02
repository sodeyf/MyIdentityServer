using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Serilog;

namespace IdentityServer.Extentions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.InjectRazorPagesAndApi()
                        .InjectUnitOfWork()
                        .AddCors(options =>
                        {
                            options.AddPolicy("AllowFlutterOrigins", policy =>
                            {
                                policy.WithOrigins(
                                        "http://192.168.0.116:6900",
                                        "http://localhost:6900")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                            });
                        })
                        .InjectIdp(builder.Configuration);





        //builder.Services.InjectIdp(builder.Configuration);

        //builder.Services.AddIdentityServer(options =>
        //    {
        //        // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
        //        options.EmitStaticAudienceClaim = true;
        //    })

        //    //.AddInMemoryIdentityResources(Config.IdentityResources)
        //    //.AddInMemoryApiScopes(Config.ApiScopes)
        //    //.AddInMemoryClients(Config.Clients)
        //    .AddConfigurationStore(options =>
        //    {
        //        options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        //            sql => sql.MigrationsAssembly(migrationsAssembly));
        //    })
        //    .AddOperationalStore(options =>
        //    {
        //        options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        //            sql => sql.MigrationsAssembly(migrationsAssembly));
        //    })
        //    .AddTestUsers(TestUsers.Users);



        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Use CORS for flutter
        app.UseCors("AllowFlutterOrigins");

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        InitializeDatabase(app);

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
