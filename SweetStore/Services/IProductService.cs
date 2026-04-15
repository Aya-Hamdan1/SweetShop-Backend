using SweetStore.Model.Products;
using SweetStore.ViewModels;
using SweetStore.ViewModels.Product;

namespace SweetStore.Services
{
    public interface IProductService
    {
        Task<Guid> CreateProduct(CreateProductDto dto);

        Task<PagedResponseDto<ProductResponseDto>> GetProducts(
        Guid? CategoryId,
        int page = 1,
        int pageSize = 5);

        Task<Product> UpdateProduct(Guid ProductId, UpdateProductDto dto);

        Task DeleteProduct(Guid id);

    }
}
