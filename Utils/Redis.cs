using System;
using StackExchange.Redis;

namespace store.Utils
{
    public class Redis : IRedis
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