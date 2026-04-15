namespace SweetStore.ViewModels.Order
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; }
    }

    public class OrderItemResponseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;
    }
}
