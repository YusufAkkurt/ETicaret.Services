using Core.Application.Abstractions.Tokens;
using Core.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Infrastructure.Services.Tokens;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken(int minute)
    {
        Token token = new();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.UtcNow.AddMinutes(minute);

        JwtSecurityToken jwtSecurityToken = new(
            audience: _configuration["Token:Audience"], 
            issuer: _configuration["Token:Issuer"],
            notBefore: DateTime.UtcNow, 
            expires: token.Expiration,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }
}
