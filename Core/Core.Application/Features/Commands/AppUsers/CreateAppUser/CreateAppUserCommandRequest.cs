namespace Core.Application.Features.Commands.AppUsers.CreateAppUser;

public class CreateAppUserCommandRequest : IRequest<CreateAppUserCommandResponse>
{
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}