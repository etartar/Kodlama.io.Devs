using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Queries.Login
{
    public class LoginQuery : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginedDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public LoginQueryHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<LoginedDto> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);

                _authBusinessRules.UserDoesNotExistWhenLoginRequested(user);
                _authBusinessRules.UserVerifyPassword(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                return new LoginedDto
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken,
                };
            }
        }
    }
}
