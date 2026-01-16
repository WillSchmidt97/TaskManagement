using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;
using TaskManagement.Api.Helpers;

namespace TaskManagement.Api.Extensions
{
    public static class ControllersExtensions
    {
        public static void AddControllersSettings(this IServiceCollection services)
        {
            // Add MVC with JSON options
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // Serialize enums as strings
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                    // Ignore null properties in JSON output
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .AddMvcOptions(options =>
                {
                    // Allow empty request bodies for model binding
                    options.AllowEmptyInputInBodyModelBinding = true;
                });

            // Enable Gzip response compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            // Set Gzip compression level to fastest
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            // Configuração de rotas em caixa baixa
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false; // opcional — mantém parâmetros de query como estão
            });

            // Add controllers with filters and input formatters
            services.AddControllers(options =>
            {
                // Support plain text input in controllers
                options.InputFormatters.Insert(options.InputFormatters.Count, new TextPlainInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                // Prevent circular references in JSON serialization
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Register IHttpContextAccessor for accessing the current HTTP context
            services.AddHttpContextAccessor();
        }
    }
}
