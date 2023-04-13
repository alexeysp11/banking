using Banking.Common.Models; 
using Microsoft.Extensions.Configuration; 

namespace Banking.Common 
{
    public class Configurator 
    {
        /// <summary>
        /// Gets settings for server from config file 
        /// </summary>
        public CoreServerSettings GetCoreServerConfigSettings(string filename)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(filename)
                .AddEnvironmentVariables()
                .Build();
            return config.GetSection("CoreServerSettings").Get<CoreServerSettings>();
        }
    }
}