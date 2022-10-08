using Application.Features.UserProfileLinks.Dtos;
using Application.Features.UserProfileLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserProfileLinks.Commands.UpdateUserProfileLink
{
    public class UpdateUserProfileLinkCommand : IRequest<UpdatedUserProfileLinkDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public class UpdateUserProfileLinkCommandHandler : IRequestHandler<UpdateUserProfileLinkCommand, UpdatedUserProfileLinkDto>
        {
            private readonly IUserProfileLinkRepository _userProfileLinkRepository;
            private readonly IMapper _mapper;
            private readonly UserProfileLinkBusinessRules _userProfileLinkBusinessRules;

            public UpdateUserProfileLinkCommandHandler(IUserProfileLinkRepository userProfileLinkRepository, IMapper mapper, UserProfileLinkBusinessRules userProfileLinkBusinessRules)
            {
                _userProfileLinkRepository = userProfileLinkRepository;
                _mapper = mapper;
                _userProfileLinkBusinessRules = userProfileLinkBusinessRules;
            }

            public async Task<UpdatedUserProfileLinkDto> Handle(UpdateUserProfileLinkCommand request, CancellationToken cancellationToken)
            {
                await _userProfileLinkBusinessRules.UserProfileLinkNameCanNotBeDuplicatedWhenUpdated(request.Id, request.Name);

                UserProfileLink? userProfileLink = await _userProfileLinkRepository.GetAsync(x => x.Id == request.Id);

                _userProfileLinkBusinessRules.UserProfileLinkShouldExistWhenRequested(userProfileLink);

                userProfileLink.UserId = request.UserId;
                userProfileLink.Name = request.Name;
                userProfileLink.Link = request.Link;

                UserProfileLink updateUserProfileLink = await _userProfileLinkRepository.UpdateAsync(userProfileLink);
                UpdatedUserProfileLinkDto updatedUserProfileLinkDto = _mapper.Map<UpdatedUserProfileLinkDto>(updateUserProfileLink);

                return updatedUserProfileLinkDto;
            }
        }
    }
}
