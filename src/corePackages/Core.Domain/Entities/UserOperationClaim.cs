using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class UserOperationClaim : Entity
    {
        public UserOperationClaim()
        {
        }

        public UserOperationClaim(int id, int userId, int operationClaimId) : this()
        {
            Id = id;
            UserId = userId;
            OperationClaimId = operationClaimId;
        }

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
