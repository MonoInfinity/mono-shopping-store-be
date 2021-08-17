using System.IO;
using store.Utils.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


namespace store.Utils
{

    public class Config : IConfig
    {
        private readonly IWebHostEnvironment env;
        public Config(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string getEnvByKey(string name)
        {
            string currentEnv = this.env.EnvironmentName.ToLower();
            string envFileName = "env." + currentEnv + ".json";
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "config") + "/" + envFileName;

            IConfiguration configs = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(envPath, true, true).Build();
            return configs[name];
        }
    }
}