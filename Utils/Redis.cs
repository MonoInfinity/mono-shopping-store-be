using System;
using StackExchange.Redis;

namespace store.Utils
{
    public class Redis : IRedis
    {

        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase redisDB;

        public Redis(IConfig config)
        {
            redis = ConnectionMultiplexer.Connect(
                     new ConfigurationOptions
                     {
                         EndPoints = { config.getEnvByKey("REDIS_URL") }
                     });

            redisDB = redis.GetDatabase();
        }

        public bool setByValue(string key, string value)
        {
            redisDB.StringSet(key, value);
            string result = redisDB.StringGet(key);
            if (!result.Equals(value)) return false;
            return true;
        }

        public string getByKey(string key)
        {
            return redisDB.StringGet(key);
        }

        public bool deleteByKey(string key)
        {
            return redisDB.KeyDelete(key);
        }
    }
}