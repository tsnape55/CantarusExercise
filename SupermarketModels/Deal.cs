namespace SupermarketModels
{
    public class Deal
    {
        public Deal(string sku, int count, float price)
        {
            Sku = sku;
            Count = count;
            Price = price;
        }

        public string Sku { get; }
        public int Count { get; }
        public float Price { get; }
    }
}
