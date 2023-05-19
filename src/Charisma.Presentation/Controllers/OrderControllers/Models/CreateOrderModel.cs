namespace Charisma.Presentation.Controllers.OrderControllers.Models;

public class CreateOrderModel
{
    public byte? DiscountPercent { get; init; }
    public int? DiscountPrice { get; init; }
    public OrderDetailModel[] Details { get; init; }
}
