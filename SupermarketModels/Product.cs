namespace SupermarketModels
{
    public class Product
    {

        public Product(string sku, float price)
        {
            Sku = sku;
            Price = price;
        }

        public string Sku { get; }
        public float Price { get; }
    }
}
