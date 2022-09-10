using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Rules
{
    public class UserProfileLinkBusinessRules
    {
        private readonly IUserProfileLinkRepository _userProfileLinkRepository;

        public UserProfileLinkBusinessRules(IUserProfileLinkRepository userProfileLinkRepository)
        {
            _userProfileLinkRepository = userProfileLinkRepository;
        }

        public async Task UserProfileLinkNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<UserProfileLink> result = await _userProfileLinkRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("User profile link name exist.");
        }

        public async Task UserProfileLinkNameCanNotBeDuplicatedWhenUpdated(int id, string name)
        {
            IPaginate<UserProfileLink> result = await _userProfileLinkRepository.GetListAsync(x => x.Id != id && x.Name == name);
            if (result.Items.Any()) throw new BusinessException("User profile link name exist.");
        }

        public void UserProfileLinkShouldExistWhenRequested(UserProfileLink? userProfileLink)
        {
            if (userProfileLink == null) throw new BusinessException("Requested user profile link does not exist.");
        }
    }
}
