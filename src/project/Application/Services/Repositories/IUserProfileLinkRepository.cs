using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IUserProfileLinkRepository : IAsyncRepository<UserProfileLink>, IRepository<UserProfileLink>
    {
    }
}
