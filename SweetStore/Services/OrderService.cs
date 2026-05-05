using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model;
using SweetStore.Model.Order;
using SweetStore.Model.Products;
using SweetStore.ViewModels;
using SweetStore.ViewModels.Order;
using System.Security.Claims;

namespace SweetStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto dto, Guid userId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CustomerPhone = dto.CustomerPhone,
                Address = dto.Address,
                Items = new List<OrderItem>(),
                CreatedAt = DateTime.UtcNow,
                TotalPrice = 0,
                Status = OrderStatus.Pending
            };
            decimal totalPrice = 0;
            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with Id {item.ProductId} not found");
                    //return BadRequest($"Product with Id {item.ProductId} not found");
                }
                var orderItem = new OrderItem
                {
                    Id = new Guid(),
                    ProductId = item.ProductId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    Price = product.Price,
                };
                await _context.OrderItems.AddAsync(orderItem);
                totalPrice += product.Price * item.Quantity;

                order.Items.Add(orderItem);
            }
            order.TotalPrice = totalPrice;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<PagedResponseDto<OrderResponseDto>> GetOrders(
        Guid? userId,
        DateTime? startDate,
        DateTime? endDate,
        int page = 1,
        int pageSize = 5)
        {
            var query = _context.Orders.AsQueryable();

            // 🔍 Filter by customer
            if (userId != null)
            {
                query = query.Where(o => o.UserId == userId);
            }

            // 📅 Filter by date
            if (startDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt <= endDate.Value);
            }

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    CustomerName = o.User.Name,
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt,
                    Items = o.Items.Select(i => new OrderItemResponseDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResponseDto<OrderResponseDto>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = orders
            };
        }

        public async Task<OrderResponseDto> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    CustomerName = o.User.Name,
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt,
                    Items = o.Items.Select(i => new OrderItemResponseDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception($"Not found");

            return order;
        }

        public async Task<bool> UpdatOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found");

            order.Status = status;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
