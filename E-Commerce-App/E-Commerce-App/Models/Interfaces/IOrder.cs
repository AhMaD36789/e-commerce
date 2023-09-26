namespace E_Commerce_App.Models.Interfaces
{
    public interface IOrder
    {
        public Task<Order> Create(Order order);

        public Task<List<Order>> GetAll();
    }
}
