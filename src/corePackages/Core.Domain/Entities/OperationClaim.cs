using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class OperationClaim : Entity
    {
        public OperationClaim()
        {
        }

        public OperationClaim(int id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
