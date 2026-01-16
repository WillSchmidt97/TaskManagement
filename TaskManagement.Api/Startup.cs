using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TaskManagement.Api.Extensions;
using TaskManagement.Domain.Settings.GlobalAppSettings;

namespace TaskManagement.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.LoadParameters(configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// 
        public void ConfigureServices(IServiceCollection services)
        {
            // Register services and repositories.
            services.RegisterAppServices();
            services.RegisterRepositories();

            // Add dependencies settings.
            services.AddDatabase();
            services.AddControllersSettings();
            services.AddMapster();
            services.AddSwaggerService();
            services.AddEndpointsApiExplorer();
            services.AddCorsSettings();
            services.AddHttpLogging(logging => { });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.ConfigureSwaggerApplication();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseRouting();
            //app.UseAuthentication();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
