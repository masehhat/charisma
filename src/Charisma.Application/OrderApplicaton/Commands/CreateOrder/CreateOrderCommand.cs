using Charisma.Application.Common.Views;
using Charisma.Domain;
using MediatR;

namespace Charisma.Application.OrderApplicaton.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OperationResult<int>>
{
    public (int productId, short itemCount)[] Details { get; set; }
    public Percent DiscountPercent { get; set; }
    public Price DiscountPrice { get; set; }
    public string UserId { get; set; }
}