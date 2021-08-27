namespace store.Src.Providers.Smail.Interface
{
    public interface ISmailService
    {
        private void SendEmail(string receiver, string content)
        { }
        public void SendOTP(string receiver, string OTP);
        public void SendOTPForUpdateEmail(string receiver, string OTP);
    }
}