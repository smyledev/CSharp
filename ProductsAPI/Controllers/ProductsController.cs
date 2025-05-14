using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet("GetAllProducts")]
        public List<Product> GetAllProducts()
        //public IEnumerable<string> Get()
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));
            return Products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("GetProductById/{id}")]
        public Product GetProductById(int id)
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product(1, "CF", "chicken", 150));
            Products.Add(new Product(2, "CF", "potato", 50));
            return Products.Find(item => item.id == id);
            //return new Product(id, "CF", "potato", 50);
        }

        // POST api/<ProductsController>
        [HttpPost("CreateProduct")]
        public void CreateProduct([FromBody]int price)
        {
            Product NewProduct = new Product(1, "CF", "potato", price);
        }

        // PUT api/<ProductsController>/5
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

        // DELETE api/<ProductsController>/5
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
