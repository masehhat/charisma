namespace Charisma.Domain;

public class OrderDetail
{
    private OrderDetail()
    {
    }

    public OrderDetail(int productId, short itemCount)
    {
        ProductId = productId;
        ItemCount = itemCount;
    }

    public int Id { get; }
    public int OrderId { get; }
    public int ProductId { get; }
    public short ItemCount { get; }
}