using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Products;
using SweetStore.ViewModels.Product;

namespace SweetStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;

        public CategoryService(AppDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        //  Create Category
        public async Task<Guid> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var imagePath = await _imageService.UploadImageAsync(dto.Image);

            var category = new Category
            {
                Id = Guid.NewGuid(), 
                Name = dto.Name,
                Description = dto.Description,
                ImgUrl = imagePath
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }

        //  Get Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
