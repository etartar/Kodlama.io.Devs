using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserProfileLinkRepository : EfRepositoryBase<UserProfileLink, BaseDbContext>, IUserProfileLinkRepository
    {
        public UserProfileLinkRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
