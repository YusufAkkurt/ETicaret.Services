namespace Core.Application.Features.Commands.AppUsers.LoginAppUser;

public class LoginAppUserCommandRequest : IRequest<LoginAppUserCommandResponse>
{
    public string? UserNameOrEmail { get; set; }
    public string? Password { get; set; }
}
