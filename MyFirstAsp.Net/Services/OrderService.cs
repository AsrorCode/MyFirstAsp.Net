using MyFirstAsp.Net.Interfaces;
using MyFirstAsp.Net.Models;

namespace MyFirstAsp.Net.Services
{
    public class  IOrderService
    {
        private readonly IOrderRepository _repository;

        public IOrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await _repository.GetAllAsync();

        public async Task<Order> GetOrderByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateOrderAsync(Order order) => await _repository.CreateAsync(order);

        public async Task UpdateOrderAsync(int id, Order order) => await _repository.UpdateAsync(order);

        public async Task DeleteOrderAsync(int id) => await _repository.DeleteAsync(id);
    }
}