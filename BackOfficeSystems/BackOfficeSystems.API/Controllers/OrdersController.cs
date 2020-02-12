using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeSystems.API.Data;
using BackOfficeSystems.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BackOfficeSystems.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController :ControllerBase
    {
        private readonly IOrderRepository repo;

        public OrdersController(IOrderRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await repo.GetOrders();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrders(List<OrderToCreateDto> orders)
        {
            await repo.AddOrders(orders);

            return StatusCode(201);
        }

        [HttpGet("quantity")]
        public async Task<IActionResult> GetBrandItemsQuantity()
        {
            var quantity = await repo.GetBrandItemsQuantity();

            return Ok(quantity);
        }

        [HttpPost("file")]
        public async Task<IActionResult> AddOrders(OrderFromFileDto orderFromFileDto)
        {
            await repo.AddOrders(orderFromFileDto);

            return StatusCode(201);
        }
    }
}