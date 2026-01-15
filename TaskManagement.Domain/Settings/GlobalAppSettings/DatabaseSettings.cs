namespace TaskManagement.Domain.Settings.GlobalAppSettings
{
    public class DatabaseSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Schema { get; set; } = string.Empty;
        public bool IncludeErrorDetail { get; set; } = false;

        public string GetConnectionString()
        {
            var searchPath = string.IsNullOrWhiteSpace(Schema) ? "" : $"SearchPath={Schema};";
            return $"Host={Host};Port={Port};Username={User};Password={Password};Database={Name};{searchPath}Pooling=true;IncludeErrorDetail={IncludeErrorDetail};";
        }
    }
}
