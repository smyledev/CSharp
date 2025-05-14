using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));
            return Products;
        }

        [HttpGet("GetProductById/{id}")]
        public Product GetProductById(int id)
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));
            return Products.Find(item => item.id == id);
        }

        [HttpPost("CreateProduct")]
        public void CreateProduct([FromBody]int price)
        {
            Product NewProduct = new Product(1, "CF", "potato", price);
        }

        [HttpPut("UpdateProduct/{id}")]
        public void UpdateProduct(int id, [FromBody]int price)
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));

            var existing = Products.FirstOrDefault(x => x.id == id);
            if (existing == null)
                return;

            existing.price = price;
        }

        [HttpDelete("DeleteProductById/{id}")]
        public void DeleteProductById(int id)
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));

            var existing = Products.FirstOrDefault(x => x.id == id);
            if (existing == null)
                return;

            Products.Remove(existing);
        }
    }
}
