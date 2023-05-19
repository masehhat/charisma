using Charisma.Application.Common.Interfaces;
using Charisma.Application.Common.Views;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Charisma.Application.IdentityApplication.Commands.CreateToken;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, OperationResult<string>>
{
    private readonly ICharismaDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateTokenCommandHandler(ICharismaDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<OperationResult<string>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        IdentityUser user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
            throw new AuthenticationException();

        bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
            throw new AuthenticationException();

        byte[] tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
          {
             new Claim(ClaimTypes.Name, user.UserName),
             new Claim(ClaimTypes.NameIdentifier, user.Id),
          }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string accessToken = tokenHandler.WriteToken(securityToken);

        return OperationResult<string>.BuildSuccess(accessToken);
    }
}