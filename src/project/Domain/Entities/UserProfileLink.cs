using Core.Domain.Entities;
using Core.Domain.Entities.Base;

namespace Domain.Entities
{
    public class UserProfileLink : Entity
    {
        public UserProfileLink()
        {
        }

        public UserProfileLink(int id, int userId, string name, string link)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Link = link;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public virtual User User { get; set; }
    }
}
