using Charisma.Domain;

namespace Charisma.Test;

public class PriceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WhenDefineTwoSamePrices_ThenTheyShouldEqual()
    {
        Price price1 = new(1200);
        Price price2 = new(1200);

        Assert.AreEqual(price1,price2);            
    }

    [TestCase(1200,800,2000)]
    [TestCase(3000,5000,8000)]
    public void WhenTwoPricePlus_ThenResultSumOfThem(decimal amount1,decimal amount2, decimal expected)
    {
        Price price1 = new(amount1);
        Price price2 = new(amount2);
        Price result = price1 + price2;

        Assert.AreEqual(result.Value, expected);
    }


    [TestCase(1200, 800, 400)]
    [TestCase(5000, 3000, 2000)]
    public void WhenTwoPriceminus_ThenResultSubtractOfThem(decimal amount1, decimal amount2, decimal expected)
    {
        Price price1 = new(amount1);
        Price price2 = new(amount2);
        Price result = price1 - price2;

        Assert.AreEqual(result.Value, expected);
    }
}
