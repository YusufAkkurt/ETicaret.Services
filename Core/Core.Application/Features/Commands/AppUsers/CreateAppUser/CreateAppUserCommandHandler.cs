using Core.Application.Exceptions;
using Core.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Features.Commands.AppUsers.CreateAppUser;

public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public CreateAppUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(new() { NameSurname = request.NameSurname, UserName = request.UserName, Email = request.Email }, request.Password);

        var response = new CreateAppUserCommandResponse(succeeded: result.Succeeded);

        if (result.Succeeded)
            response.Message = "Kullanıcı başarıyla oluşturuldu.";
        else
            foreach (var error in result.Errors)
                response.Message = $"{error.Description}\n";

        return response;
    }
}
