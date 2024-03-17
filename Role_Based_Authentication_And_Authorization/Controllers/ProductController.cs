using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Authentication_And_Authorization.Models.Domain;
using Role_Based_Authentication_And_Authorization.Models.Dto;
using Role_Based_Authentication_And_Authorization.Repositories.Interface;

namespace Role_Based_Authentication_And_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto request)
        {
            // Here mapping DTO to Domain model
            var product = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
            };

            await productRepository.CreateAsync(product);


            // Here Domain model to DTO
            var response = new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
            };
            return Ok(response);
        }

        //GET : /api/products

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAscyn();

            //map Domain model in Dto

            var response = new List<ProductDto>();
            foreach (var product in products)
            {
                response.Add(new ProductDto { ProductId = product.ProductId, ProductName = product.ProductName, ProductDescription = product.ProductDescription });
            }

            return Ok(response);
        }
    }
}
