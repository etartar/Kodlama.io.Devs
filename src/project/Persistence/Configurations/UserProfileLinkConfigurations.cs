using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserProfileLinkConfigurations : IEntityTypeConfiguration<UserProfileLink>
    {
        public void Configure(EntityTypeBuilder<UserProfileLink> builder)
        {
            builder.ToTable("UserProfileLinks");
            builder.HasKey(up => up.Id);
            builder.Property(up => up.Id).HasColumnName("Id");
            builder.Property(up => up.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(up => up.Name).HasColumnName("Name").IsRequired();
            builder.Property(up => up.Link).HasColumnName("Link").IsRequired();

            builder.HasOne(up => up.User);
        }
    }
}
