//using Duende.IdentityServer.Services;
//using Duende.IdentityServer.Stores;

//namespace IdentityServer.Extentions
//{
//    public static class IdentityServerEntityFrameworkBuilderExtensions
//    {
//        public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder)
//        {
//            builder.Services.AddTransient<IClientStore, ClientStoreBusiness>();
//            builder.Services.AddTransient<IResourceStore, ResourceStoreBusiness>();
//            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyBusiness>();

//            return builder;
//        }

//        public static IIdentityServerBuilder AddOperationalStore(this IIdentityServerBuilder builder)
//        {
//            //builder.Services.AddSingleton<TokenCleanupBusiness>();
//            //builder.Services.AddSingleton<IHostedService, Business.IdpBusiness.TokenCleanupHost>();
//            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStoreBusiness>();

//            return builder;
//        }
//    }
//}
