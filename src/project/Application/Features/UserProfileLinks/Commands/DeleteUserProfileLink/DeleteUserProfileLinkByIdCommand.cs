using Application.Features.UserProfileLinks.Dtos;
using Application.Features.UserProfileLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserProfileLinks.Commands.DeleteUserProfileLink
{
    public class DeleteUserProfileLinkByIdCommand : IRequest<DeletedUserProfileLinkDto>
    {
        public DeleteUserProfileLinkByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class DeleteUserProfileLinkByIdCommandHandler : IRequestHandler<DeleteUserProfileLinkByIdCommand, DeletedUserProfileLinkDto>
        {
            private readonly IUserProfileLinkRepository _userProfileLinkRepository;
            private readonly IMapper _mapper;
            private readonly UserProfileLinkBusinessRules _userProfileLinkBusinessRules;

            public DeleteUserProfileLinkByIdCommandHandler(IUserProfileLinkRepository userProfileLinkRepository, IMapper mapper, UserProfileLinkBusinessRules userProfileLinkBusinessRules)
            {
                _userProfileLinkRepository = userProfileLinkRepository;
                _mapper = mapper;
                _userProfileLinkBusinessRules = userProfileLinkBusinessRules;
            }

            public async Task<DeletedUserProfileLinkDto> Handle(DeleteUserProfileLinkByIdCommand request, CancellationToken cancellationToken)
            {
                UserProfileLink? userProfileLink = await _userProfileLinkRepository.GetAsync(x => x.Id == request.Id);

                _userProfileLinkBusinessRules.UserProfileLinkShouldExistWhenRequested(userProfileLink);

                UserProfileLink deletedUserProfileLink = await _userProfileLinkRepository.DeleteAsync(userProfileLink);
                DeletedUserProfileLinkDto deletedUserProfileLinkDto = _mapper.Map<DeletedUserProfileLinkDto>(deletedUserProfileLink);

                return deletedUserProfileLinkDto;
            }
        }
    }
}
