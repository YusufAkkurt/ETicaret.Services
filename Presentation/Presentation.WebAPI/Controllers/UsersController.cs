using Core.Application.Features.Commands.AppUsers.CreateAppUser;
using Core.Application.Features.Commands.AppUsers.LoginAppUser;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppUser(CreateAppUserCommandRequest request) => Ok(await _mediator.Send(request));

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginAppUserCommandRequest request) => Ok(await _mediator.Send(request));
}
