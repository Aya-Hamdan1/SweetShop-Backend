using SweetStore.Model.Order;
using SweetStore.ViewModels;
using SweetStore.ViewModels.Order;

namespace SweetStore.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(CreateOrderDto dto, Guid userId);
        Task<PagedResponseDto<OrderResponseDto>> GetOrders(
        Guid? userId,
        DateTime? startDate,
        DateTime? endDate,
        int page = 1,
        int pageSize = 5);
        Task<OrderResponseDto> GetOrder(Guid id);
        Task<bool> UpdatOrderStatus(Guid OrderId, OrderStatus status);
    }
}
