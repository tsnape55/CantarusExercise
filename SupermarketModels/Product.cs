namespace SupermarketModels
{
    public class Product
    {

        public Product(string sku, float price)
        {
            Sku = sku;
            Price = price;
        }

        public string Sku { get; private set; }
        public float Price { get; private set; }
    }
}
