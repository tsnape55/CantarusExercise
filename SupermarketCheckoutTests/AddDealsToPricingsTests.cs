using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Assert.
            // No exception was thrown.
            Assert.IsTrue(true);
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
