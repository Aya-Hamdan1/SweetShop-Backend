namespace SweetStore.ViewModels.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; }

    }
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public IFormFile? Image { get; set; } // 👈 اختياري
    }
}
