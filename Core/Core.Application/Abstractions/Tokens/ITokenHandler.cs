using Core.Application.DTOs;

namespace Core.Application.Abstractions.Tokens;

public interface ITokenHandler
{
    Token CreateAccessToken(int minute);
}
