using Microsoft.AspNetCore.Mvc;
using MyFirstAsp.Net.Interfaces;
using MyFirstAsp.Net.Models;
using MyFirstAsp.Net.Services;

namespace MyFirstAsp.Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
          

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetOrderByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _service.CreateOrderAsync(order);
            return Ok("Muvaffaqiyatli yaratildi!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            await _service.UpdateOrderAsync(id, order);
            return Ok("Muvaffaqiyatli yangilandi!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteOrderAsync(id);
            return Ok("O'chirib tashlandi!");
        }
    }
}