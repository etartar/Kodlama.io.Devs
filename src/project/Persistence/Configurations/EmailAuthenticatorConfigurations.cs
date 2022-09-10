using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EmailAuthenticatorConfigurations : IEntityTypeConfiguration<EmailAuthenticator>
    {
        public void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
        {
            builder.ToTable("EmailAuthenticators");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(e => e.ActivationKey).HasColumnName("ActivationKey").IsRequired(false);
            builder.Property(e => e.IsVerified).HasColumnName("IsVerified").IsRequired();

            builder.HasOne(e => e.User);
        }
    }
}
