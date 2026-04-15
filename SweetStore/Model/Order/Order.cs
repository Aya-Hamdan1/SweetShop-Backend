namespace SweetStore.Model.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } 
        public ICollection<OrderItem> Items { get; set; }

    }
}
