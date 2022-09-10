using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("Id");
            builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(r => r.Token).HasColumnName("Token").IsRequired();
            builder.Property(r => r.Expires).HasColumnName("Expires").IsRequired();
            builder.Property(r => r.Created).HasColumnName("Created").IsRequired();
            builder.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp").IsRequired();
            builder.Property(r => r.Revoked).HasColumnName("Revoked").IsRequired(false);
            builder.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp").IsRequired(false);
            builder.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken").IsRequired(false);
            builder.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked").IsRequired(false);

            builder.HasOne(r => r.User);
        }
    }
}
