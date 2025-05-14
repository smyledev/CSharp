namespace ProductsAPI
{
    public class Product
    {

        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public Product (int id, string sku, string name, int price)
        {
            this.id = id;
            this.sku = sku;
            this.name = name;
            this.price = price;
        }
    }
}
