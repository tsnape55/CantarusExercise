using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout;
using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;

namespace SupermarketCheckoutTests
{
    [TestClass]
    public class AddProductsToPricingsTests
    {
        [TestMethod]
        public void Add_Item_To_Pricings_Successfully()
        {
            // Arrange.
            const string sku = "DIY0001";
            const float price = 6.99f;
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct(sku, price);
            var pricing = pricings.GetProductBySku("DIY0001");

            // Assert.
            // No exception was thrown.
            Assert.IsTrue(pricing.Sku == sku);
            Assert.IsTrue(pricing.Price == price);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateProductException))]
        public void Add_Duplicate_Item_To_Pricings_Succesfully_Throws_DuplicateProductException()
        {

            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", 6.99f);
            pricings.AddProduct("DIY0001", 6.99f);

            // Assert.
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSkuException))]
        public void Add_Item_To_Pricings_With_Empty_Sku_Throws_DuplicateProductException()
        {

            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("", 6.99f);

            // Assert.
            Assert.Fail();
        }


        [TestMethod]
        [ExpectedException(typeof(DuplicateProductException))]
        public void Add_Duplicate_Item_With_Different_Case_To_Pricings_Throws_DuplicateProductException()
        {
            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", 6.99f);
            pricings.AddProduct("DIy0001", 6.99f);

            // Assert.
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(PriceZeroOrLessException))]
        [DataRow(-6.99f)]
        [DataRow(0f)]
        [DataRow(-10.6665f)]
        [DataRow(-900000000.6665f)]
        public void Add_Item_To_Pricings_With_0_Or_Less_Price_Throws_PriceZeroOrLessException(float value)
        {
            // Arrange.
            IPricings pricings = new Pricings();

            // Act
            pricings.AddProduct("DIY0001", value);

            // Assert.
            Assert.Fail();
        }
    }
}
