namespace store.Utils
{
    public interface IRedis
    {
        public bool setByValue(string key, string value);
        public string getByKey(string key);
        public bool deleteByKey(string key);
    }
}