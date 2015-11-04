using Sermo.Data.Contracts;
using System.Configuration;
namespace Sermo.Infrastructure.Contracts
{
    public class ConfigAppSettings : IApplicationSettings
    {
        public string GetValue(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}