using TaskManagement.Domain.Settings.GlobalAppSettings;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            // EF Core DataContext
            services.AddDbContext<Data.Context.AppDbContext>(options =>
            {
                options.UseNpgsql(AppSettings.Database.GetConnectionString());
            });
        }
    }
}
