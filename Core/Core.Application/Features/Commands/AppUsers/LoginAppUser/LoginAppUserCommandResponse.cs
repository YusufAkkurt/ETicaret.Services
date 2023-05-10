using Core.Application.DTOs;

namespace Core.Application.Features.Commands.AppUsers.LoginAppUser;

public class LoginAppUserCommandResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public Token? Token { get; set; }

    public LoginAppUserCommandResponse() { }

    public LoginAppUserCommandResponse(bool succeeded)
    {
        Succeeded = succeeded;
    }

    public LoginAppUserCommandResponse(bool succeeded, string? message) : this(succeeded)
    {
        Message = message;
    }
}