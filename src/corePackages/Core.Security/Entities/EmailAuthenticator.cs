using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class EmailAuthenticator : Entity
    {
        public EmailAuthenticator()
        {
        }

        public EmailAuthenticator(int id, int userId, string? activationKey, bool isVerified) : base(id)
        {
            UserId = userId;
            ActivationKey = activationKey;
            IsVerified = isVerified;
        }

        public int UserId { get; set; }
        public string? ActivationKey { get; set; }
        public bool IsVerified { get; set; }

        public virtual User User { get; set; }
    }
}
