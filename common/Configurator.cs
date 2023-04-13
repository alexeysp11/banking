using Microsoft.Extensions.Configuration; 

namespace Banking.Common 
{
    public class Configurator 
    {
        /// <summary>
        /// Gets settings from config file 
        /// </summary>
        public Settings GetConfigSettings(string filename)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .AddEnvironmentVariables()
                .Build();
            return config.GetSection("Settings").Get<Settings>();
        }
    }
}