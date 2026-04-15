namespace SweetStore.Model.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
        public string ImgUrl { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public Category Category { get; set; }
    }
}
