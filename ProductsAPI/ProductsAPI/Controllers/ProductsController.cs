using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, ProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            return _productsService.GetAllProducts();
        }

        [HttpGet("GetProductById/{id}")]
        public Product? GetProductById(int id)
        {
            return _productsService.GetProductById(id);
        }

        [HttpPost("CreateProduct")]
        public void CreateProduct(string name, int price)
        {
            _productsService.CreateProduct(name, price);
        }

        [HttpPut("UpdateProduct/{id}")]
        public void UpdateProduct(int id, [FromBody]int price)
        {
            _productsService.UpdateProduct(id, price);
        }

        [HttpDelete("DeleteProductById/{id}")]
        public void DeleteProductById(int id)
        {
            _productsService.DeleteProductById(id);
        }
    }
}
