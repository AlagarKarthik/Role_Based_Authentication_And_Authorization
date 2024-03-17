using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Models.Dto;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            // Here mapping DTO to Domain model
            var category = new Category
            {
                CategoryName = request.CategoryName,
                CategoryDescription = request.CategoryDescription,
            };

            await categoryRepository.CreateAsync(category);


            // Here Domain model to DTO
            var response = new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
            };
            return Ok(response);
        }

        //GET : /api/categories

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await categoryRepository.GetAllAscyn();

            //map Domain model in Dto

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto { CategoryId = category.CategoryId, CategoryName = category.CategoryName, CategoryDescription = category.CategoryDescription });
            }

            return Ok(response);
        }
    }
}
