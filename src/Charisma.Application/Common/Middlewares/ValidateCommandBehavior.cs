using Charisma.Application.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Charisma.Application.Common.Middlewares
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidateCommandBehavior(IValidator<TRequest> validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validator == null)
                return await next();

            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid)
                return await next();

            List<string> errorCodes = validationResult.Errors.Select(x => x.ErrorCode).ToList();

            string errors = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            throw new CommandValidationException($"Invalid command, reason: {errors}");
        }
    }
}