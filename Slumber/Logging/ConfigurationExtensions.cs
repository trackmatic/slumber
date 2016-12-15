namespace Slumber.Logging
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseConsoleLogger(this ISlumberConfiguration configuration)
        {
            configuration.Log = new ConsoleLogger();
            return configuration;
        }
    }
}
