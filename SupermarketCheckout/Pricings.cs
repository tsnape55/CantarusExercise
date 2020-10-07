using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;
using SupermarketModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Pricings : IPricings
    {
        private readonly List<Deal> _deals;

        public Pricings()
        {
            Products = new List<Product>();
            _deals = new List<Deal>();
        }

        public List<Product> Products { get; }

        public void AddDeal(string sku, int count, float price)
        {

            var existingProduct = GetProductBySku(sku);

            if (existingProduct == null)
            {
                throw new InvalidSkuException("A Product with that sku has not been added.");
            }

            if (count <= 1)
            {
                throw new DealCountLessThanTwoException();
            }

            if (price <= 0)
            {
                throw new PriceZeroOrLessException();
            }

            _deals.Add(new Deal(sku, count, price));
        }

        public void AddProduct(string sku, float price)
        {

            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new InvalidSkuException("Sku must not be an empty string");
            }

            if (price <= 0)
            {
                throw new PriceZeroOrLessException();
            }

            var existingProduct = GetProductBySku(sku);

            if (existingProduct != null)
            {
                throw new DuplicateProductException($"Product with the sku {existingProduct.Sku} already exists");
            }

            Products.Add(new Product(sku, price));
        }

        public IEnumerable<Deal> GetDealsBySku(string sku) => _deals.Where(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

        public Product GetProductBySku(string sku) => Products.FirstOrDefault(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
    }
}
