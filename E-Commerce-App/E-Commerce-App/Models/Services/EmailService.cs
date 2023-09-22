using E_Commerce_App.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace E_Commerce_App.Models.Services
{
    public class EmailService : IEmail
    {
        private readonly string _apiKey = "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"; // replace with your actual API key

        public async Task SendEmail()
        {
            var fromAddress = new MailAddress("ahmadsa28121999@gmail.com", "ahmad saleh");
            var toAddress = new MailAddress("ahmadsa28121999@gmail.com", "ahmad saleh");
            const string fromPassword = "4OhJ0cD3HY5vgdCq";
            const string subject = "Enter the subject here";
            const string body = "<strong>Hello</strong>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
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
