using Mapster;

namespace TaskManagement.Api.Extensions
{
    public static class MapsterExtensions
    {
        public static void AddMapster(this IServiceCollection services)
        {
            // Add Mapster configuration to the service collection.
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(config);
        }
    }
}
