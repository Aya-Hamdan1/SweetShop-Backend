using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Order;
using SweetStore.Model.Products;
using SweetStore.ViewModels;
using SweetStore.ViewModels.Order;

namespace SweetStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                Id = new Guid(),
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                CustomerPhone = dto.CustomerPhone,
                Address = dto.Address,
                Items = new List<OrderItem>(),
                CreatedAt = DateTime.UtcNow,
                TotalPrice = 0,
                Status = "pending"
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
        string? customerName,
        DateTime? startDate,
        DateTime? endDate,
        int page = 1,
        int pageSize = 5)
        {
            var query = _context.Orders.AsQueryable();

            // 🔍 Filter by customer
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o => o.CustomerName.Contains(customerName));
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
                    CustomerName = o.CustomerName,
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
                    CustomerName = o.CustomerName,
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


    }
}
