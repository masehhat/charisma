using Charisma.Application.Common.Views;
using Charisma.Application.OrderApplicaton.Commands.CreateOrder;
using Charisma.Domain;
using Charisma.Presentation.Controllers.OrderControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charisma.Presentation.Controllers.OrderControllers;


[Route(OrderRoutes.BaseRoute)]
public class OrderController : AppBaseController
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator,IHttpContextAccessor httpContextAccessor)
        :base(httpContextAccessor)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OperationResult<int>), 200)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderModel model)
    {
        OperationResult<int> result = await _mediator.Send(new CreateOrderCommand
        {
            UserId = UserId,
            Details = model.Details.Select(x => (x.ProductId, x.ItemCount)).ToArray(),
            DiscountPercent = model.DiscountPercent.HasValue ? new Percent(model.DiscountPercent.Value) : null,
            DiscountPrice = model.DiscountPrice.HasValue ? new Price(model.DiscountPrice.Value) : null,
        });

        return Ok(result);
    }
}
