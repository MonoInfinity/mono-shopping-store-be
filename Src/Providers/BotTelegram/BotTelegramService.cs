using System;
using System.Net.Http;
using System.Threading.Tasks;
using store.Src.Providers.BotTelegram.Interface;
using store.Src.Utils.Interface;

namespace store.Src.Providers.BotTelegram
{
    public class BotTelegramService : IBotTelegramServiceAsync
    {
        private readonly IConfig config;
        public BotTelegramService(IConfig config)
        {
            this.config = config;
        }
        public void send(string data)
        {
            sendAsync(data).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseMessage> sendAsync(string data)
        {
            string BOT_TOKEN = this.config.getEnvByKey("BOT_TOKEN");
            string CHAT_ID = this.config.getEnvByKey("CHAT_ID");
            string uri = $"https://api.telegram.org/bot{BOT_TOKEN}/sendMessage?chat_id={CHAT_ID}&text={data}";
           
            HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            return responseMessage;
        }
    }
}