using System.Configuration;
using Contracts;

namespace UI
{
    public class ConfigAppSettings : IApplicationSettings
    {
        public string GetValue(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}