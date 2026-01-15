using Microsoft.Extensions.Configuration;

namespace TaskManagement.Domain.Settings.GlobalAppSettings
{
    public abstract class AppSettings
    {
        public static string BuildTime { get; set; } = string.Empty;
        public static string AllowedHosts { get; set; } = string.Empty;
        public static DatabaseSettings Database { get; set; } = new();
        public static string Environment { get; set; } = string.Empty;
        public static SystemSettings System { get; set; } = new();

        /// <summary>
        /// Carrega todas as propriedades estáticas no AppSettings.
        /// </summary>
        public static void LoadParameters(IConfiguration configuration)
        {
            BuildTime = DateTime.UtcNow.AddHours(-3).ToString("dd/MM/yyyy HH:mm:ss");
            AllowedHosts = configuration.GetSection(nameof(AllowedHosts))?.Value?.ToString() ?? "*";
            Database = configuration.GetSection(nameof(Database))?.Get<DatabaseSettings>() ?? new DatabaseSettings();
            Environment = configuration.GetSection(nameof(Environment))?.Value?.ToString() ?? "Production";
            System = configuration.GetSection(nameof(System))?.Get<SystemSettings>() ?? new SystemSettings();
        }
    }
}
