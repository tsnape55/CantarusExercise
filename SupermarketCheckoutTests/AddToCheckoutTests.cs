using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;
using System;
using System.Linq;

namespace SupermarketCheckoutTests
{

    [TestClass]
    public class AddToCheckoutTests
    {
        [TestMethod]
        public void Can_Scan_Products_Configured_In_Pricings()
        {

            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            ICheckout checkout = new Checkout();

            //Act 
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("B");

            // Assert.
            var productA = checkout.Lines.FirstOrDefault(x => x.Sku.Equals("A", StringComparison.OrdinalIgnoreCase));
            var productB = checkout.Lines.FirstOrDefault(x => x.Sku.Equals("B", StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(productA.Quantity == 1);
            Assert.IsTrue(productB.Quantity == 1);
        }

        [TestMethod]
        public void Can_Scan_Multiple_Of_The_Same_Products_Configured_In_Pricings()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            ICheckout checkout = new Checkout();

            //Act 
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            // Assert.
            var productA = checkout.Lines.FirstOrDefault(x => x.Sku.Equals("A", StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(productA.Quantity == 4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSkuException))]
        public void Scanning_Product_Not_Configured_In_Pricings_Throws_InvalidSkuException()
        {
            // Arrange.
            ICheckout checkout = new Checkout();
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            checkout.Configure(pricing);

            // Act
            checkout.Scan("B");

            // Assert.
            Assert.Fail();
        }

        [TestMethod]
        public void Can_Remove_Scanned_Item_From_Checkout_Lines()
        {

            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            ICheckout checkout = new Checkout();

            //Act 
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Remove("A");

            // Assert.
            var productA = checkout.Lines.FirstOrDefault(x => x.Sku.Equals("A", StringComparison.OrdinalIgnoreCase));

            Assert.IsNull(productA);
        }


        [TestMethod]
        public void Can_Remove_Multiple_Scanned_Item_From_Checkout_Lines()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            ICheckout checkout = new Checkout();

            //Act 
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Remove("A");
            checkout.Remove("A");

            // Assert.
            var productA = checkout.Lines.FirstOrDefault(x => x.Sku.Equals("A", StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(productA.Quantity == 1);
        }

        [TestMethod]
        public void Can_Remove_All_Scanned_Items_From_Checkout_Lines()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 5f);
            pricing.AddProduct("C", 5f);
            ICheckout checkout = new Checkout();

            //Act 
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Empty();
            
            // Assert.
            Assert.IsTrue(!checkout.Lines.Any());
        }
    }
}
