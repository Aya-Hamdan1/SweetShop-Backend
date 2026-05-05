using SweetStore.Model.Order;
using System.ComponentModel.DataAnnotations;

namespace SweetStore.ViewModels.Order
{
    public class CreateOrderDto
    {
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }
        public string Address { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public ICollection<OrderItemDto> Items { get; set; }

    }

    public class OrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
