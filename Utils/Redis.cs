using StackExchange.Redis;
using store.Utils.Interface;

namespace store.Utils
{
    public class Redis : IRedisHelper
    {

        private readonly ConnectionMultiplexer redis;
        public Redis(IConfig config)
        {
            redis = ConnectionMultiplexer.Connect(
                     new ConfigurationOptions
                     {
                         EndPoints = { config.getEnvByKey("REDIS_URL") }
                     });

        }

    }
}