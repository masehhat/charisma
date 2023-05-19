using Charisma.Domain.OrderAgrregate;
using System.Runtime;
using Tools;

namespace Charisma.Domain;

public class Order
{
    private Order()
    {

    }
    public Order(string userId,Price netPrice, OrderDetail[] details,bool hasBreakableProduct)
    {
        UserId = userId;
        State = OrderDeliveryState.Ready;

        var currentTime = DateTime.Now.TimeOfDay;

        if (currentTime < new TimeSpan(8, 0, 0) || currentTime > new TimeSpan(19, 0, 0))
            throw new InvalidOperationException("add order is allowed between 8AM to 7PM");

        CreatedAt = DateTime.UtcNow.ToUnixTime();

        if (details is null || !details.Any())
            throw new ArgumentNullException(nameof(details));

        if (details.Select(x => x.ProductId).Distinct().Count() != details.Length)
            throw new ArgumentException("product is duplicated");

        OrderDetails = details;
        PostType = hasBreakableProduct ? PostType.Pishtaz : PostType.Normal;
        NetPrice = netPrice;
        DiscountPrice = Price.Zero;
    }

    public Order(string userId,Price netPrice, OrderDetail[] details, bool hasBreakableProduct, Price discountPrice)
        : this(userId, netPrice,details,hasBreakableProduct)
    {
        DiscountPrice = discountPrice;
    }

    public Order(string userId,Price netPrice, OrderDetail[] details, bool hasBreakableProduct, Percent discountPercent)
        : this(userId, netPrice,details,hasBreakableProduct)
    {
        DiscountPrice = new(NetPrice.Value / 100 * discountPercent.Value);
    }

    public int Id { get; }
    public string UserId { get; }
    public int CreatedAt { get; }
    public OrderDeliveryState State { get; private set; }
    public PostType PostType { get; }
    public Price NetPrice { get; }
    public Price DiscountPrice { get; }
    public Price FinalPrice => NetPrice - DiscountPrice;
    public ICollection<OrderDetail> OrderDetails { get; }
    
    public void AdvanceState()
    {
        State = State switch
        {
            OrderDeliveryState.Ready => OrderDeliveryState.InProgress,
            OrderDeliveryState.InProgress => OrderDeliveryState.Sent,
            OrderDeliveryState.Sent => OrderDeliveryState.Delivered,
            _ => throw new InvalidOperationException("current state is not valid to advance"),
        };
    }

    public void Cancel()
    {
        if (State != OrderDeliveryState.Ready)
            throw new InvalidOperationException("cancelation is allowed olny in ready state");

        State = OrderDeliveryState.Cancel;
    }
}
