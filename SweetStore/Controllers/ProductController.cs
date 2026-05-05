using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Model.Products;
using SweetStore.Services;
using SweetStore.ViewModels.Product;

namespace SweetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
           _productService = productService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var id = await _productService.CreateProduct(dto);

            return Ok(new { Success = true, Message = "Product created successfully", Data = id });

        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(
        Guid? CategoryId,
        int page = 1,
        int pageSize = 5)
        {
            var result = await _productService.GetProducts(CategoryId, page, pageSize);
            return Ok(new { Success = true, Message = "Products retrieved", Data = result });

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> UpdateProduct(Guid ProductId, [FromForm] UpdateProductDto dto)
        {
           var product = _productService.UpdateProduct(ProductId, dto);
           return Ok(new { Success = true, Message = "Product updated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _productService.DeleteProduct(id);
            return Ok(new { Success = true, Message = "Product Deleted Successfully" });
        }
    }

}
