namespace SweetStore.Model.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } 
        public ICollection<OrderItem> Items { get; set; }

    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
}

