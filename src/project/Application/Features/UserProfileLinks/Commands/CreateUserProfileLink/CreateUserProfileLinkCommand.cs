using Application.Features.UserProfileLinks.Dtos;
using Application.Features.UserProfileLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserProfileLinks.Commands.CreateUserProfileLink
{
    public class CreateUserProfileLinkCommand : IRequest<CreatedUserProfileLinkDto>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public class CreateUserProfileLinkCommandHandler : IRequestHandler<CreateUserProfileLinkCommand, CreatedUserProfileLinkDto>
        {
            private readonly IUserProfileLinkRepository _userProfileLinkRepository;
            private readonly IMapper _mapper;
            private readonly UserProfileLinkBusinessRules _userProfileLinkBusinessRules;

            public CreateUserProfileLinkCommandHandler(IUserProfileLinkRepository userProfileLinkRepository, IMapper mapper, UserProfileLinkBusinessRules userProfileLinkBusinessRules)
            {
                _userProfileLinkRepository = userProfileLinkRepository;
                _mapper = mapper;
                _userProfileLinkBusinessRules = userProfileLinkBusinessRules;
            }

            public async Task<CreatedUserProfileLinkDto> Handle(CreateUserProfileLinkCommand request, CancellationToken cancellationToken)
            {
                await _userProfileLinkBusinessRules.UserProfileLinkNameCanNotBeDuplicatedWhenInserted(request.Name);

                UserProfileLink userProfileLink = _mapper.Map<UserProfileLink>(request);
                UserProfileLink createdUserProfileLink = await _userProfileLinkRepository.AddAsync(userProfileLink);
                CreatedUserProfileLinkDto createdUserProfileLinkDto = _mapper.Map<CreatedUserProfileLinkDto>(createdUserProfileLink);

                return createdUserProfileLinkDto;
            }
        }
    }
}
