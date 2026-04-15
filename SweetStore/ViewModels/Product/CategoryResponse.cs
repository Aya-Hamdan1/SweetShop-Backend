namespace SweetStore.ViewModels.Product
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; } // 👈 اختياري
    }
    //public class CategoryResponseDto
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public string ImgUrl { get; set; }
    //}
}
