using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyFirstAsp.Net.Data;
using MyFirstAsp.Net.Models;
using System.Data;

namespace MyFirstAsp.Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DapperContext _context;
   

        public OrderController(DapperContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT * FROM Orders";

            using var connection = _context.CreateConnection();

            var result = await connection.QueryAsync<Order>(query);

            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = "SELECT * FROM Orders WHERE Id=@Id";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<Order>(query, new { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var query = "INSERT INTO Orders (ProductName, Quantity) VALUES (@ProductName, @Quantity)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, order);

            return Ok("Created");
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            var query = "UPDATE Orders SET ProductName=@ProductName, Quantity=@Quantity WHERE Id=@Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new
            {
                order.ProductName,
                order.Quantity,
                Id = id
            });

            return Ok("Updated");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = "DELETE FROM Orders WHERE Id=@Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });

            return Ok("Deleted");
        }
    }
}