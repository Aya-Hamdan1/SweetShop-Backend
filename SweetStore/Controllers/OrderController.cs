using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Order;
using SweetStore.Services;
using SweetStore.ViewModels.Order;

namespace SweetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(new { Success = true, Message = "Order Created Successfully", Data = order });
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders(
            string? customerName,
            DateTime? startDate,
            DateTime? endDate,
            int page = 1,
            int pageSize = 5)
        {
            var orders = await _orderService.GetOrders(customerName,startDate,endDate,page = 1,pageSize = 5);
            return Ok(orders);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrder(id);

            return Ok(order);
        }
    }
}
