namespace mono_store_be.AuthModule.Entity
{
    public class ReToken
    {
        public string reTokenId { get; set; }
        public string data { get; set; }
        public string userId { get; set; }

        public ReToken(){
            this.reTokenId = "";
            this.data = "";
            this.userId = "";
        }

        public override string ToString()
        {
            return "Retoken: \nReTokenId: " + reTokenId + " \nData: " + data + " \nUserId: " + userId;
        }
    }
}