# Supermarket Checkout

## Task
Implement the code for a supermarket checkout that calculates the total price of a shopping basket.

Our goods are priced individually. In addition, some items are multipriced: buy *n* of them, and they’ll cost you *y* cents. For example, item 'A' might cost 50 cents individually, but this week we have a special offer: buy three 'A's and they'll cost you $1.30. In fact this week's prices are:

| Item | Unit Price | Special Price |
|------|------------|---------------|
| A | 50 | 3 for 130 | 
| B | 30 | 2 for 45 | 
| C | 20 |  | 
| D | 15 |  | 

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we'll recognize the two B's and price them at 45 (for a total price so far of 95). Because the pricing changes frequently, we need to be able to pass in a set of pricing rules each time we start handling a checkout transaction.

The interface to the checkout should look like:
```c#
var checkout = new Checkout(pricingRules);

checkout.Scan(item1);
checkout.Scan(item2);

// etc.

int price = checkout.Total();
```

We should build some unit tests to check our implementation of this. Similar to, but more expansive than:
```c#
[TestClass]
public class SupermarketCheckoutTests
{
    [TestMethod]
    public void Scan_SingleItem_DoesNotError()
    {
        // Arrange
        var rules = new Rules();
        var checkout = new Checkout(rules);
        var item = new Item();

        // Act
        checkout.Scan(item);

        // Assert no exception is thrown
        Assert.IsTrue(true);
    }

    // multiple items scanned etc.

    [TestMethod]
    public void Total_BasketScanned_TotalCorrect()
    {
        // Arrange
        var rules = new Rules();
        var checkout = new Checkout(rules);
        
        // Scan a bunch of items here

        // Act
        int actualTotal = checkout.Total();

        // Assert
        Assert.IsEqual(expectedTotal, actualTotal);        
    }

    // More basket scenarios here
    // Think multi buy offers and stuff    
}
```

Following from this we should add different types of offers, think about how we might best structure our Basket, Checkout and Products. Try and make everything contain that little bit more complexity.