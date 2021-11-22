using System;

namespace Shopperior.Logging
{
    public interface IShopperiorLogger
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);
        void LogFatal(string message);
        void LogFatal(Exception ex, string message);
    }
}