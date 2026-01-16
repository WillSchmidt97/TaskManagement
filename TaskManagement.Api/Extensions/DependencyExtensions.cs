using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Data.Repositories;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Api.Extensions
{
    public static class DependencyExtensions
    {
        #region AppServices
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskAppService, TaskAppService>();

            return services;
        }
        #endregion

        #region Repositories
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
        #endregion
    }
}