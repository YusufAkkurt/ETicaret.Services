namespace Core.Application.Features.Commands.AppUsers.CreateAppUser;

public class CreateAppUserCommandResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }

    public CreateAppUserCommandResponse() { }

    public CreateAppUserCommandResponse(bool succeeded)
    {
        Succeeded = succeeded;
    }

    public CreateAppUserCommandResponse(bool succeeded, string? message) : this(succeeded)
    {
        Message = message;
    }
}
