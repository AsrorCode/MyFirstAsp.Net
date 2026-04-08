using Dapper;
using MyFirstAsp.Net.Data;
using MyFirstAsp.Net.Interfaces;
using MyFirstAsp.Net.Models;

namespace MyFirstAsp.Net.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var query = "SELECT * FROM Orders";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Order>(query);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Orders WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Order>(query, new { Id = id });
        }

        public async Task CreateAsync(Order order)
        {
            var query = "INSERT INTO Orders (ProductName, Quantity) VALUES (@ProductName, @Quantity)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, order);
        }

        public async Task UpdateAsync(Order order)
        {
            var query = "UPDATE Orders SET ProductName=@ProductName, Quantity=@Quantity WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, order);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Orders WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}