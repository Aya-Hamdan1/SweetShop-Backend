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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto dto)
        {
            var id = await _categoryService.CreateCategoryAsync(dto);

            return Ok(new
            {
                Success = true,
                Message = "Category created successfully",
                Data = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(new
            {
                Success = true,
                Message = "Categories retrieved",
                Data = categories
            });
        }

    }
}
