using Core.Application.Abstractions.Tokens;
using Core.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Features.Commands.AppUsers.LoginAppUser;

public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, LoginAppUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginAppUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
        user ??= await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        var response = new LoginAppUserCommandResponse(succeeded: false);

        if (user is null)
        {
            response.Message = "Kullanıcı adı veya şifre hatalı!";
            return response;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded is false)
        {
            response.Message = "Kimlik doğrulanırken bir hata oluştu!";
            return response;
        }

        response.Succeeded = result.Succeeded;
        response.Message = "Giriş işlemi başarılı";
        response.Token = _tokenHandler.CreateAccessToken(5);

        return response;
    }
}
