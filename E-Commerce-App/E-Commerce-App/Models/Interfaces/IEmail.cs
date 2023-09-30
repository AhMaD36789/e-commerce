namespace E_Commerce_App.Models.Interfaces
{
    public interface IEmail
    {
        public Task SendEmail(string reciever);

        public Task SendEmailOrderSummery(string recieverEmail, string recieverName, Order order);
    }
}
