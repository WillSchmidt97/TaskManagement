using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManagement.Api.Helpers
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = "TaskManagement RESTful API",
                    Version = description.ApiVersion.ToString(),
                    Description = $"Build Time: <strong>{DateTime.UtcNow.AddHours(-3):dd/MM/yyyy HH:mm:ss}</strong> (BRT)<br/>" +
                                  $"<br/><br/>TASK MANAGEMENT<strong></strong><br/>" +
                                  $"<a href=\"https://gamepartyfinder.com.br\" target=\"_blank\">www.taskmanagement.com.br</a>"
                });
            }
        }
    }
}
