using SweetStore.ViewModels;
using SweetStore.ViewModels.Order;

namespace SweetStore.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderDto dto);
        Task<PagedResponseDto<OrderResponseDto>> GetOrders(
        string? customerName,
        DateTime? startDate,
        DateTime? endDate,
        int page = 1,
        int pageSize = 5);
        public async Task<OrderResponseDto> GetOrder(Guid id);
    }
}
