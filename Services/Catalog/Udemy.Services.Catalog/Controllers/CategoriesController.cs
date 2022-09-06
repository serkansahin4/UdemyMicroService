using Microsoft.AspNetCore.Mvc;
using Udemy.Services.Catalog.Dtos;
using Udemy.Services.Catalog.Services;
using Udemy.Shared.ControllerBases;

namespace Udemy.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> Get(CategoryCreateDto categoryCreateDto)
        {
            var result = await _categoryService.CreateAsync(categoryCreateDto);
            return CreateActionResultInstance(result);
        }
    }
}
