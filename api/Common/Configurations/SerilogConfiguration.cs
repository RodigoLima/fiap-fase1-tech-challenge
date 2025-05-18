using Serilog;

namespace fiap_fase1_tech_challenge.Common.Configurations
{
    public class SerilogConfiguration
    {
        public static void SetupLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
