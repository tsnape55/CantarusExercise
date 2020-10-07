using SupermarketModels;
using System.Collections.Generic;

namespace SupermarketInterfaces
{
    public interface IPricings
    {
        /// <summary>
        /// Add a new product to the pricings.
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="price"></param>
        void AddProduct(string sku, float price);

        /// <summary>
        /// Add a new deal to the pricings.
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        void AddDeal(string sku, int count, float price);

        /// <summary>
        /// Return a Product that has been added.
        /// </summary>
        /// <param name="sku"></param>
        Product GetProductBySku(string sku);

        /// <summary>
        /// Return all Deals where the product sku is present
        /// </summary>
        /// <param name="sku"></param>
        IEnumerable<Deal> GetDealsBySku(string sku);

        /// <summary>
        /// Returns the currently configured products
        /// </summary>
        /// <returns></returns>
        List<Product> Products { get; }
    }
}
