using Application.Features.UserProfileLinks.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.UserProfileLinks.Models
{
    public class UserProfileLinkModel : BasePageableModel
    {
        public IList<UserProfileLinkListDto> Items { get; set; }
    }
}
