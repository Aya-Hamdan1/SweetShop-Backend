
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Products;
using SweetStore.ViewModels;
using SweetStore.ViewModels.Product;

namespace SweetStore.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        public ProductService(AppDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<Guid> CreateProduct(CreateProductDto dto)
        {
            var imagePath = await _imageService.UploadImageAsync(dto.Image);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ImgUrl = imagePath,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;

        }

        public async Task<PagedResponseDto<ProductResponseDto>> GetProducts(
        Guid? CategoryId,
        int page = 1,
        int pageSize = 5)
        {
            var query = _context.Products.AsQueryable();

            //  فلترة حسب الكاتيجوري إذا موجود
            if (CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == CategoryId);
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImgUrl = p.ImgUrl,
                    Description = p.Description,
                })
                .ToListAsync();

            return new PagedResponseDto<ProductResponseDto>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = products
            };
        }

        public async Task<Product> UpdateProduct(Guid ProductId, UpdateProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);
            if (product == null)
                throw new Exception($"Product not found");

            product.Name = dto.Name?.Trim();
            product.Description = dto.Description?.Trim();
            product.Price = dto.Price;
            if (dto.Image != null)
            {
                _imageService.DeleteImage(product.ImgUrl);

                product.ImgUrl = await _imageService.UploadImageAsync(dto.Image);
            }
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new Exception($"Product not found");

            var isUsed = await _context.OrderItems
                .AnyAsync(i => i.ProductId == id);

            if (isUsed)
                throw new Exception($"Cannot delete product");
            if (!string.IsNullOrEmpty(product.ImgUrl))
                _imageService.DeleteImage(product.ImgUrl);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();


        }



    }
}
