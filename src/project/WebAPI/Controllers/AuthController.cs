using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetListOperationClaimByUserId;
using Application.Features.Users.Queries.LoginUser;
using AutoMapper;
using Core.Domain.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public AuthController(ITokenHelper tokenHelper, IMapper mapper)
        {
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserQuery)
        {
            LoginUserDto result = await Mediator.Send(loginUserQuery);

            User mappedUser = _mapper.Map<User>(result);

            AccessToken token = await CreateAccessToken(mappedUser);

            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
        {
            CreatedUserDto result = await Mediator.Send(registerUserCommand);

            User mappedUser = _mapper.Map<User>(result);

            AccessToken token = await CreateAccessToken(mappedUser);

            return Created("", token);
        }

        private async Task<AccessToken> CreateAccessToken(User user)
        {
            IList<OperationClaim> operationClaims = await Mediator.Send(new GetListOperationClaimByUserIdQuery(user.Id));

            AccessToken createAccessToken = _tokenHelper.CreateToken(user, operationClaims);

            return createAccessToken;
        }
    }
}
