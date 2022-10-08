using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserOperationClaimConfigurations : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims");
            builder.HasKey(uoc => uoc.Id);
            builder.Property(uoc => uoc.Id).HasColumnName("Id");
            builder.Property(uoc => uoc.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(uoc => uoc.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();

            builder.HasOne(uoc => uoc.User);
            builder.HasOne(uoc => uoc.OperationClaim);
        }
    }
}
