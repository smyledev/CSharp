using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI
{
    public class ProductsService
    {
        public static List<Product> Products = new List<Product> {
        new Product(1, "CF", "chicken", 150),
        new Product(2, "CF", "potato", 50) };

        public List<Product> GetAllProducts()
        {
            return Products;
        }
        public Product? GetProductById(int id)
        {
            return Products.Find(item => item.id == id);
        }

        public void CreateProduct(string name, int price)
        {
            Products.Add(new Product(1, "CF", name, price));
        }

        public void UpdateProduct(int id, int price)
        {
            var existing = Products.FirstOrDefault(x => x.id == id);
            if (existing == null)
                return;

            existing.price = price;
        }

        public void DeleteProductById(int id)
        {
            var existing = Products.FirstOrDefault(x => x.id == id);
            if (existing == null)
                return;

            Products.Remove(existing);
        }

    }
}
