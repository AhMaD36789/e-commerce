using E_Commerce_App.Models.Interfaces;
using System.Net;
using System.Net.Mail;

namespace E_Commerce_App.Models.Services
{
    public class EmailService : IEmail
    {
        private readonly string _apiKey = "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"; // replace with your actual API key

        public async Task SendEmail(string reciever)
        {
            var fromAddress = new MailAddress("MjhemPatata@gmail.com", "Tech Store");
            var toAddress = new MailAddress(reciever, reciever.Substring(0, reciever.IndexOf("@")));
            const string fromPassword = "4OhJ0cD3HY5vgdCq";
            const string subject = "Order Summary";
            const string body = "<strong>Hello</strong>";

            var authAddress = new MailAddress("ahmadsa28121999@gmail.com", "ahmad saleh");

            var smtp = new SmtpClient
            {
                Host = "smtp-relay.brevo.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(authAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                message.Headers.Add("ApiKey", _apiKey); // add the API key to the email headers

                await smtp.SendMailAsync(message);
            }
        }
    }
}
