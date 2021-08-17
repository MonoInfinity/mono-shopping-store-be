using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Reflection;

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

        public bool setByKey(string key, string value)
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

        public void setOjectByKey(string key, object obj)
        {
            redisDB.HashSet(key, this.toHashEntries(obj));
            Console.WriteLine(redisDB.HashGetAll(key));
        }

        public HashEntry[] toHashEntries(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            var entries = new HashEntry[properties.Length];
            int i = 0;
            foreach (var property in properties)
            {
                object propertyValue = property.GetValue(obj);
                entries[i++] = new HashEntry(property.Name, property.GetValue(obj).ToString());
            }
            return entries;
        }

        // public T ConvertFromRedis<T>(HashEntry[] hashEntries)
        // {
        //     PropertyInfo[] properties = typeof(T).GetProperties();
        //     var obj = Activator.CreateInstance(typeof(T));
        //     int i = 0;
        //     foreach (var property in properties)
        //     {
        //         HashEntry entry = hashEntries.ToDictionary(g => g.Name.ToString().Equals(property.Name));
        //         if (entry.Equals(new HashEntry())) continue;
        //         property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
        //     }
        //     return (T)obj;
        // }

    }
}