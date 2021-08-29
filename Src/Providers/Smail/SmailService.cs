using SendGrid;
using SendGrid.Helpers.Mail;
using store.Src.Providers.Smail.Interface;
using store.Src.Utils.Interface;

namespace store.Src.Providers.Smail
{
    public class SmailService : ISmailService
    {
        private readonly IConfig config;
        public SmailService(IConfig config)
        {
            this.config = config;
        }
        private void SendEmail(string receiver, string content)
        {
            var apiKey = this.config.getEnvByKey("SENDGRID_SECRET");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.config.getEnvByKey("FROM_MAIL"));
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(receiver);
            var htmlContent = "<div>" +
                          "<p>" + content + "</p>" +
                          "</br>" +
                          "<p>Thanks,</p>" +
                          "<p>Mono Team</p>" +
                        "</div>;";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendOTP(string receiver, string OTP)
        {
            this.SendEmail(receiver,
                           "<div>" +
                                 "<h2>Hi " + receiver + "</h2>" +
                                 "<>We're received a request to reset the password for the Mono shop account. No changes have been made to your account yet.</p>" +
                                 "</br>" +
                                 "<p>You can reset your password by clicking the link: <a href=\"" + this.config.getEnvByKey("CLIENT_URL") + "/user/reset-password/" + OTP + ">Click here</a></p>" +
                                 "</br>" +
                                 "<p>If you did not request a new password, you can safely ignore this email. Please do not share this email, link, or access code with others</p>" +
                           "</div>"
                           );
        }

        public void SendOTPForUpdateEmail(string receiver, string OTP)
        {
            this.SendEmail(receiver,
                           "<div>" +
                                 "<h2>Hi " + receiver + "</h2>" +
                                 "<>We're received a request toto update the email for the Mono shop account. No changes have been made to your account yet.</p>" +
                                 "</br>" +
                                 "<p>You can reset your password by clicking the link: <a href=\"" + this.config.getEnvByKey("CLIENT_URL") + "/user/reset-password/" + OTP + ">Click here</a></p>" +
                                 "</br>" +
                                 "<p>If you did not request a new password, you can safely ignore this email. Please do not share this email, link, or access code with others</p>" +
                           "</div>"
                           );
        }
    }
}