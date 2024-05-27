using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public readonly IOrderManagementService _orderService;

        public OrdersController(IOrderManagementService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult> Post(OrderDTO order)
        {
            return Ok(await _orderService.CreateOrder(order));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Get()
        {
            return Ok(await _orderService.GetOrders());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _orderService.GetOrder(id));
        }

        [HttpGet("search/{searchByOrderName}")]
        public async Task<ActionResult> GetByOrderName(string searchByOrderName)
        {
            return Ok(await _orderService.GetOrdersByOrderName(searchByOrderName));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(OrderDTO order, int id)
        {
            return Ok(await _orderService.EditOrder(order, id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _orderService.DeleteOrder(id));
        }
    }
}
