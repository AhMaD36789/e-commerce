using E_Commerce_App.Models.Interfaces;

namespace E_Commerce_App.Models.Services
{
    public class EmailService : IEmail
    {
        public Task SendEmailAsync(string email, string subject, string htmlMassege)
        {
            throw new NotImplementedException();
        }
    }
}
