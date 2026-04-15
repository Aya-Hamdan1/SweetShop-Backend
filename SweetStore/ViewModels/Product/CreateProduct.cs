using System.ComponentModel.DataAnnotations;

namespace SweetStore.ViewModels.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
