namespace Charisma.Presentation.Controllers.OrderControllers.Models;

public record OrderDetailModel
{
    public int ProductId { get; init; }
    public short ItemCount { get; init; }
}
