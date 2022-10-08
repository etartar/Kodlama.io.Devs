using Application.Features.UserProfileLinks.Commands.CreateUserProfileLink;
using Application.Features.UserProfileLinks.Commands.DeleteUserProfileLink;
using Application.Features.UserProfileLinks.Commands.UpdateUserProfileLink;
using Application.Features.UserProfileLinks.Dtos;
using Application.Features.UserProfileLinks.Models;
using Application.Features.UserProfileLinks.Queries.GetListByUserIdUserProfileLink;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileLinksController : BaseController
    {
        [HttpGet("ByUserId/{userId}")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, int userId)
        {
            UserProfileLinkModel userProfileLinkModel = await Mediator.Send(new GetListByUserIdUserProfileLinkQuery
            {
                PageRequest = pageRequest,
                UserId = userId
            });

            return Ok(userProfileLinkModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserProfileLinkCommand createUserProfileLinkCommand)
        {
            CreatedUserProfileLinkDto result = await Mediator.Send(createUserProfileLinkCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserProfileLinkCommand updateUserProfileLinkCommand)
        {
            UpdatedUserProfileLinkDto result = await Mediator.Send(updateUserProfileLinkCommand);
            return Created("", result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedUserProfileLinkDto result = await Mediator.Send(new DeleteUserProfileLinkByIdCommand(id));
            return Created("", result);
        }
    }
}
