using System;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Shopperior.Logging
{
    public class ShopperiorLogger : IShopperiorLogger
    {
        public ShopperiorLogger(ShopperiorLoggerConfiguration configuration)
        {
            Log.Logger = InitializeLogger(configuration).CreateLogger();
        }

        public void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void LogError(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public void LogFatal(string message)
        {
            Log.Fatal(message);
        }

        public void LogFatal(Exception ex, string message)
        {
            Log.Fatal(ex, message);
        }

        private LoggerConfiguration InitializeLogger(ShopperiorLoggerConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", configuration.ApplicationName);
            SetMinimumLogLevel(loggerConfig, configuration);
            if (configuration.SqlServerConfiguration != null)
                SetSqlServerConfiguration(loggerConfig, configuration.SqlServerConfiguration);

            return loggerConfig;
        }

        private void SetMinimumLogLevel(
            LoggerConfiguration loggerConfiguration,
            ShopperiorLoggerConfiguration smLoggerConfiguration)
        {
            switch (smLoggerConfiguration.MinimumLogLevel)
            {
                case ShopperiorLogLevel.Debug:
                    loggerConfiguration.MinimumLevel.Debug();
                    break;
                case ShopperiorLogLevel.Information:
                    loggerConfiguration.MinimumLevel.Information();
                    break;
                case ShopperiorLogLevel.Warning:
                    loggerConfiguration.MinimumLevel.Warning();
                    break;
                case ShopperiorLogLevel.Error:
                    loggerConfiguration.MinimumLevel.Error();
                    break;
                default:
                    loggerConfiguration.MinimumLevel.Fatal();
                    break;
            }
        }

        private void SetSqlServerConfiguration(
            LoggerConfiguration loggerConfiguration,
            ShopperiorLoggerSqlServerConfiguration sqlServerConfiguration)
        {
            loggerConfiguration.WriteTo.MSSqlServer(
                connectionString: sqlServerConfiguration.ConnectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    AutoCreateSqlTable = true,
                    TableName = sqlServerConfiguration.TableName
                });
        }
    }
}