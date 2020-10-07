using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;

namespace SupermarketCheckoutTests
{

    [TestClass]
    public class CheckoutConfigurationTests
    {
        [TestMethod]
        public void Can_Configure_Checkout()
        {
            // Arrange.
            IPricings pricings = new Pricings();
            pricings.AddProduct("A", 6.99f);

            // Act
            ICheckout checkout = new Checkout();
            checkout.Configure(pricings);

        }

        [TestMethod]
        [ExpectedException(typeof(CheckoutNotConfiguredException))]
        public void Configure_With_Null_Pricings_Throws_CheckoutNotConfiguredException()
        {
            // Arrange.
            ICheckout checkout = new Checkout();

            // Act
            checkout.Configure(null);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckoutNotConfiguredException))]
        public void Configure_With_Empty_Products_Pricings_Throws_CheckoutNotConfiguredException()
        {
            // Arrange.
            ICheckout checkout = new Checkout();
            IPricings pricings = new Pricings();

            // Act
            checkout.Configure(pricings);

            //Assert
            Assert.Fail();
        }


        [TestMethod]
        [ExpectedException(typeof(CheckoutNotConfiguredException))]
        public void Scannning_Product_When_Not_Configured_Throws_CheckoutNotConfiguredException()
        {
            // Arrange.
            ICheckout checkout = new Checkout();

            // Act
            checkout.Scan("A");

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckoutNotConfiguredException))]
        public void Get_Savings_When_Pricings_Not_Configured_Throws_CheckoutNotConfiguredException()
        {
            // Arrange.
            ICheckout checkout = new Checkout();

            // Act
            checkout.Savings();

            //Assert
            Assert.Fail();
        }
    }
}
