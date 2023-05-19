using Charisma.Application.Common.Views;
using Charisma.Application.IdentityApplication.Commands.CreateToken;
using Charisma.Presentation.Controllers.IdentityControllers.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Charisma.Presentation.Controllers.IdentityControllers;

[Route(IdentityRoutes.BaseRoute)]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(IdentityRoutes.LoginRoute)]    
    [ProducesResponseType(typeof(OperationResult<string>),200)]
    public async Task<IActionResult> GetAccessTokenAsync([FromBody] LoginModel loginModel)
    {
        OperationResult<string> result = await _mediator.Send(new CreateTokenCommand
        {
            Password = loginModel.Password,
            Username = loginModel.Username
        });
        
        return Ok(result);
    }
}
