using SweetStore.Model.Products;
using SweetStore.ViewModels.Product;

namespace SweetStore.Services
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategoryAsync(CreateCategoryDto dto);

        Task<List<Category>> GetCategoriesAsync();
    }
}
