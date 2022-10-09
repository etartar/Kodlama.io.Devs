using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userRepository = userRepository;
        }

        public async Task UserOperationClaimOperationClaimIdCanNotBeDuplicatedWhenInserted(int userId, int operationClaimId)
        {
            IPaginate<UserOperationClaim> result = await _userOperationClaimRepository.GetListAsync(x => x.UserId == userId && x.OperationClaimId == operationClaimId);
            if (result.Items.Any()) throw new BusinessException("OperationClaim exists for user.");
        }

        public async Task CheckUserExist(int userId)
        {
            User? user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null) throw new BusinessException("User does not exist.");
        }

        public void UserOperationClaimShouldExistWhenRequested(UserOperationClaim? userOperationClaim)
        {
            if (userOperationClaim == null) throw new BusinessException("Requested user operation claim does not exist.");
        }
    }
}
