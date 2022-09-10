using Application.Features.Users.Commands.CreateUserProfileLink;
using Application.Features.Users.Commands.DeleteUserProfileLink;
using Application.Features.Users.Commands.UpdateUserProfileLink;
using Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("UserProfileLink")]
        public async Task<IActionResult> Add([FromBody] CreateUserProfileLinkCommand createUserProfileLinkCommand)
        {
            CreatedUserProfileLinkDto result = await Mediator.Send(createUserProfileLinkCommand);

            return Created("", result);
        }

        [HttpPut("UserProfileLink")]
        public async Task<IActionResult> Update([FromBody] UpdateUserProfileLinkCommand updateUserProfileLinkCommand)
        {
            UpdatedUserProfileLinkDto result = await Mediator.Send(updateUserProfileLinkCommand);

            return Created("", result);
        }

        [HttpDelete("UserProfileLink/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedUserProfileLinkDto result = await Mediator.Send(new DeleteUserProfileLinkByIdCommand(id));
            return Created("", result);
        }
    }
}
