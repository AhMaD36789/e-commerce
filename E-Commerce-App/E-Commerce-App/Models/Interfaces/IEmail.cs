namespace E_Commerce_App.Models.Interfaces
{
    public interface IEmail
    {
        public Task SendEmailAsync(string email, string subject, string htmlMassege);
    }
}
