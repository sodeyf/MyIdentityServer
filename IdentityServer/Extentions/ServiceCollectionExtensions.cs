using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace IdentityServer.Extentions
{
    public static class ServiceCollectionExtensions
    {
        #region [Configure Service Method(s)]

        internal static IServiceCollection InjectRazorPagesAndApi(this IServiceCollection services) =>
        services.AddRazorPages()
            .Services;


        internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services)
        {
            return services;
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }


        internal static IServiceCollection InjectIdp(this IServiceCollection services, IConfiguration configuration)
        {

            var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
            return services.AddIdentityServer(options =>
               {
                   // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                   options.EmitStaticAudienceClaim = true;
               })

                  //.AddInMemoryIdentityResources(Config.IdentityResources)
                  //.AddInMemoryApiScopes(Config.ApiScopes)
                  //.AddInMemoryClients(Config.Clients)
                  .AddConfigurationStore(options =>
                  {
                      options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                          sql => sql.MigrationsAssembly(migrationsAssembly));
                  })
                  .AddOperationalStore(options =>
                  {
                      options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                          sql => sql.MigrationsAssembly(migrationsAssembly));
                  })
                  .AddTestUsers(TestUsers.Users).Services;

            //return services.AddIdentityServer(options => options.KeyManagement.Enabled = false)
            ////.AddSigningCredential(LoadCertificateFromFile(configuration))
            //.AddCustomUserStore()
            //.AddConfigurationStore()
            //.AddOperationalStore()
            //.Services;

        }


        #endregion
    }
}
