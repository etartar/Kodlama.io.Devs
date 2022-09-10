using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<LoginUserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(x => x.Email == request.Email);

                _userBusinessRules.UserDoesNotExistWhenLoginRequested(user);
                _userBusinessRules.UserVerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);

                LoginUserDto loginUserDto = _mapper.Map<LoginUserDto>(user);

                return loginUserDto;
            }
        }
    }
}
