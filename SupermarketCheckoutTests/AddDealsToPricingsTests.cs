using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;
using System.Linq;

namespace SupermarketCheckoutTests
{
    [TestClass]
    public class AddDealsToPricingsTests
    {
        [TestMethod]
        public void Add_Deal_To_Pricings_Successfully()
        {
            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", 6.99f);
            pricings.AddDeal("DIY0001", 2, 10.00f);
            pricings.AddDeal("DIY0001", 3, 12.00f);

            // Assert.

            var deals = pricings.GetDealsBySku("DIY0001");


            Assert.IsTrue(deals.Count() == 2);
            Assert.IsNotNull(deals.FirstOrDefault(x => x.Count == 2 && x.Price == 10.00f));
            Assert.IsNotNull(deals.FirstOrDefault(x => x.Count == 3 && x.Price == 12.00f));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidSkuException))]
        public void Add_Deal_With_Sku_Not_Added_Throws_InvalidSkuException()
        {
            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", 6.99f);
            pricings.AddDeal("DIY0002", 2, 10.00f);

            // Assert.
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(DealCountLessThanTwoException))]
        public void Add_Deal_With_Less_Than_Two_Quantity_Throws_DealCountLessThanTwoException()
        {
            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", 6.99f);
            pricings.AddDeal("DIY0001", 1, 10.00f);

            // Assert.
            Assert.Fail();
        }


    }
}
