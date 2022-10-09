using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetList([FromRoute] int userId)
        {
            List<UserOperationClaimListDto> userOperationClaims = await Mediator.Send(new GetListUserOperationClaimQuery
            {
                UserId = userId
            });

            return Ok(userOperationClaims);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreatedUserOperationClaimDto result = await Mediator.Send(createUserOperationClaimCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimByUserIdCommand deleteUserOperationClaimByUserIdCommand)
        {
            DeletedUserOperationClaimDto result = await Mediator.Send(deleteUserOperationClaimByUserIdCommand);

            return Created("", result);
        }
    }
}
