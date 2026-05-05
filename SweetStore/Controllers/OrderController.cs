using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Order;
using SweetStore.Services;
using SweetStore.ViewModels.Order;
using System.Security.Claims;

namespace SweetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderService.CreateOrderAsync(dto, Guid.Parse(userId));
            return Ok(new { Success = true, Message = "Order Created Successfully", Data = order });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders(
            Guid? userId,
            DateTime? startDate,
            DateTime? endDate,
            int page = 1,
            int pageSize = 5)
        {
            var orders = await _orderService.GetOrders(userId, startDate,endDate,page = 1,pageSize = 5);
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("MyOrders")]
        public async Task<IActionResult> GetMyOrders(
            DateTime? startDate,
            DateTime? endDate,
            int page = 1,
            int pageSize = 5)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _orderService.GetOrders(Guid.Parse(userId), startDate, endDate, page = 1, pageSize = 5);
            return Ok(orders);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrder(id);

            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatus orderStatus)
        {
            await _orderService.UpdatOrderStatus(id, orderStatus);

            return Ok(new { Success = true, Message = "Order status updated" });
        }
    }
}
