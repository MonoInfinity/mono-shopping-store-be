using StackExchange.Redis;

namespace store.Src.Utils
{
    public interface IRedis
    {
        public bool setByKey(string key, string value);
        public string getByKey(string key);
        public bool deleteByKey(string key);
        public void setOjectByKey(string key, object obj);
        public HashEntry[] toHashEntries(object obj);
    }
}