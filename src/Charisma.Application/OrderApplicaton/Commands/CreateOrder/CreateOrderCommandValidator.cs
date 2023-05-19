using Charisma.Application.ProductApplication.Models;
using Charisma.Application.ProductApplication.Queries.GetProducts;
using FluentValidation;
using MediatR;

namespace Charisma.Application.OrderApplicaton.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(IMediator mediator)
    {
        RuleFor(x => x)
            .Must(req => req.DiscountPercent is null || req.DiscountPrice is null)
            .WithMessage("order cant has discount by percent and price at the same time");

        RuleFor(x => x.Details)
            .NotNull()
            .WithMessage("details is null");

        RuleFor(x => x.Details)
            .MustAsync(async (details, cancellation) =>
           {
               if (!details.Any())
                   return false;

               if (details.Any(x => x.itemCount <= 0))
                   return false;

               if (details.Select(x => x.productId).Distinct().Count() != details.Length)
                   return false;

               ProductDto[] products = await mediator.Send(new GetProductsQuery
               {
                   ProductIds = details.Select(x => x.productId).ToArray()
               });

               if (products.Length != details.Length)
                   return false;

               return true;
           }).WithMessage("details are invalid");
    }
}