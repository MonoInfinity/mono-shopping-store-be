using System.Net.Http;
using System.Threading.Tasks;

namespace store.Src.Providers.BotTelegram.Interface
{
    public interface IBotTelegramService
    {
        public void send(string data);
    }

    public interface IBotTelegramServiceAsync: IBotTelegramService
    {
        public Task<HttpResponseMessage> sendAsync(string data);
    }
}

