using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.UserProfileLinks.Rules
{
    public class UserProfileLinkBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileLinkRepository _userProfileLinkRepository;

        public UserProfileLinkBusinessRules(IUserRepository userRepository, IUserProfileLinkRepository userProfileLinkRepository)
        {
            _userRepository = userRepository;
            _userProfileLinkRepository = userProfileLinkRepository;
        }

        public async Task CheckUserExist(int userId)
        {
            User? user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null) throw new BusinessException("User does not exist.");
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
