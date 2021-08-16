using System.IO;
using Microsoft.Extensions.Configuration;


namespace store.Utils.Test
{
    public class ConfigTest : IConfig
    {
        public string getEnvByKey(string name)
        {
            string envFileName = "env.testing.json";
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "config") + "/" + envFileName;

            IConfiguration configs = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(envPath, true, true).Build();
            return configs[name];
        }
    }
}