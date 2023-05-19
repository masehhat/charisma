using Charisma.Application.Common.Views;
using MediatR;

namespace Charisma.Application.IdentityApplication.Commands.CreateToken;

public class CreateTokenCommand : IRequest<OperationResult<string>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}