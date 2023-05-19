using Charisma.Application.Common.Interfaces;
using Charisma.Application.Common.Views;
using Charisma.Application.ProductApplication.Models;
using Charisma.Application.ProductApplication.Queries.GetProducts;
using Charisma.Domain;
using MediatR;

namespace Charisma.Application.OrderApplicaton.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OperationResult<int>>
{
    private readonly IMediator _mediator;
    private readonly ICharismaDbContext _context;

    public CreateOrderCommandHandler(ICharismaDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationResult<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ProductDto[] products = await _mediator.Send(new GetProductsQuery
        {
            ProductIds = request.Details.Select(x => x.productId).ToArray()
        });

        OrderDetail[] details = request.Details.Select(x => new OrderDetail(x.productId, x.itemCount)).ToArray();
        bool hasBreakableProduct = products.Any(x => x.IsBreakable);

        decimal netPrice = details.Join(products, d => d.ProductId, p => p.Id, (d, p) => new
        {
            p.Price,
            d.ItemCount
        }).Select(x => new
        {
            Amount = x.ItemCount * x.Price.Value
        }).Sum(x => x.Amount);

        Order order = new(request.UserId, new(netPrice), details, hasBreakableProduct, new Price(100));

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return OperationResult<int>.BuildSuccess(order.Id);
    }
}