using Charisma.Domain;

namespace Charisma.Test;

public class OrderTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WhenTryCreateBetween8To19_ThenShouldRaiseException()
    {
        var currentTime = DateTime.Now.TimeOfDay;
        bool inForbiddenTime = currentTime < new TimeSpan(8, 0, 0) || currentTime > new TimeSpan(19, 0, 0);

        if (!inForbiddenTime)
            Assert.Pass();
        else
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                OrderDetail[] details = { new OrderDetail(1, 10) };
                Order order = new("cc45d7cd-068b-4468-9c83-b90ed688854f", new(2000), details, true);
            });
        }
    }

    [Test]
    public void WhenDetailsIsNull_ThenShouldRaiseException()
    {
        var currentTime = DateTime.Now.TimeOfDay;
        bool inForbiddenTime = currentTime < new TimeSpan(8, 0, 0) || currentTime > new TimeSpan(19, 0, 0);

        if (inForbiddenTime)
            Assert.Pass();
        else
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                OrderDetail[] details = Array.Empty<OrderDetail>();
                Order order = new("cc45d7cd-068b-4468-9c83-b90ed688854f", new(2000), details, true);
            });
        }
    }

    [Test]
    public void WhenDetailsHaveDuplicatedProducts_ThenShouldRaiseException()
    {
        var currentTime = DateTime.Now.TimeOfDay;
        bool inForbiddenTime = currentTime < new TimeSpan(8, 0, 0) || currentTime > new TimeSpan(19, 0, 0);

        if (inForbiddenTime)
            Assert.Pass();
        else
        {
            Assert.Throws<ArgumentException>(() =>
            {
                OrderDetail[] details = { new OrderDetail(1, 10),new OrderDetail(1,5) };
                Order order = new("cc45d7cd-068b-4468-9c83-b90ed688854f", new(2000), details, true);
            });
        }
    }
}