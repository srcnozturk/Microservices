using Microsoft.AspNetCore.Mvc;
using Services.Catalog.API.Dtos;
using Services.Catalog.API.Services.Interfaces;
using Shared.ControllerBases;
using System.Threading.Tasks;

namespace Services.Catalog.API.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var categories =await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var category=await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(category);
        }
    }
}
