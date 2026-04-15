using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.ViewModels.Order;

namespace SweetStore.Services
{
    public class DashboardServices
    {
        private readonly AppDbContext _context;
        public DashboardServices(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboard()
        {
            // Total Sales
            var totalSales = await _context.Orders
                .Select(o => (decimal?)o.TotalPrice)
                .SumAsync() ?? 0;

            // Total Orders
            var totalOrders = await _context.Orders.CountAsync();

            // Best Selling Product
            var bestProduct = await _context.OrderItems
                .GroupBy(i => new { i.ProductId, i.Product.Name })
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .FirstOrDefaultAsync();

            // Today Sales
            var today = DateTime.UtcNow.Date;

            var todaySales = await _context.Orders
                .Where(o => o.CreatedAt >= today)
                .SumAsync(o => o.TotalPrice);
            // Top 3 Product 
            var topProducts = await _context.OrderItems
                .GroupBy(i => new { i.ProductId, i.Product.Name })
                .Select(g => new TopProductDto
                {
                    Name = g.Key.Name,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(3)
                .ToListAsync();

            var result = new DashboardDto
            {
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                BestProductName = bestProduct?.ProductName,
                BestProductSales = bestProduct?.TotalQuantity ?? 0,
                TodaySales = todaySales,
                TopProducts = topProducts
            };

            return result;
        }
    }
}
