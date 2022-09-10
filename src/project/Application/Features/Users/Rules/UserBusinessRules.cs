using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(x => x.Email.ToLower() == email.ToLower());
            if (result.Items.Any()) throw new BusinessException("User email exist."); 
        }

        public void CheckUserExist(User? user)
        {
            if (user == null) throw new BusinessException("User does not exist.");
        }

        public void UserDoesNotExistWhenLoginRequested(User? user)
        {
            if (user == null) throw new BusinessException("User name and/or password wrong.");
        }

        public void UserVerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            {
                throw new BusinessException("User name and/or password wrong.");
            }
        }
    }
}
