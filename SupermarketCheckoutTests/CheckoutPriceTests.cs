using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketInterfaces;

namespace SupermarketCheckoutTests
{

    [TestClass]
    public class CheckoutPriceTests
    {
        [TestMethod]
        public void Subtotal_Returns_Expected_Price()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert
            Assert.AreEqual(18.99f, checkout.Subtotal());
        }

        [TestMethod]
        public void All_Value_Functions_Return_0_When_No_Products_Scan()
        {

            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act

            //Assert
            Assert.AreEqual(0f, checkout.Savings());
            Assert.AreEqual(0f, checkout.Total());
            Assert.AreEqual(0f, checkout.Subtotal());
        }

        [TestMethod]
        public void Savings_Returns_0_When_No_Deals_Added()
        {

            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert
            Assert.AreEqual(0f, checkout.Savings());
        }

        [TestMethod]
        public void Savings_Returns_Expected_Price()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            pricing.AddDeal("A", 2, 8f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert
            Assert.AreEqual(2f, checkout.Savings());
        }

        [TestMethod]
        public void Total_Returns_Expected_Price_Multiple_Deals_Chooses_Highest_Saving()
        {

            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            pricing.AddDeal("A", 2, 8f);
            pricing.AddDeal("A", 3, 12f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            //Assert
            Assert.AreEqual(15.99f, checkout.Total());

            //Assert
            Assert.AreEqual(3f, checkout.Savings());
            Assert.AreEqual(18.99f, checkout.Subtotal());
            Assert.AreEqual(15.99f, checkout.Total());
        }

        [TestMethod]
        public void Savings_Returns_Expected_Price_Multiple_Deals()
        {
            // Arrange.
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 1f);
            pricing.AddProduct("B", 2f);
            pricing.AddDeal("A", 2, 1.50f);
            pricing.AddDeal("B", 2, 3f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            //Assert
            Assert.AreEqual(1.5F, checkout.Savings());
            Assert.AreEqual(6F, checkout.Subtotal());
            Assert.AreEqual(4.5F, checkout.Total());
        }

    }
}
