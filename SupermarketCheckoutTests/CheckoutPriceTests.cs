using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckoutTests
{

    [TestClass]
    public class CheckoutPriceTests
    {
        [TestMethod]
        public void Accurate_Price()
        {
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.AreEqual(13.99f, checkout.Subtotal());
        }

        [TestMethod]
        public void Accurate_Price_WithDeal()
        {
            IPricings pricing = new Pricings();
            pricing.AddProduct("A", 5f);
            pricing.AddProduct("B", 3.99f);
            pricing.AddDeal("A", 2, 8f);
            ICheckout checkout = new Checkout();
            checkout.Configure(pricing);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.AreEqual(11.99f, checkout.Total());
        }

    }
}
