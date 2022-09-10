﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OtpAuthenticatorConfigurations : IEntityTypeConfiguration<OtpAuthenticator>
    {
        public void Configure(EntityTypeBuilder<OtpAuthenticator> builder)
        {
            builder.ToTable("OtpAuthenticators");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("Id");
            builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(o => o.SecretKey).HasColumnName("SecretKey").IsRequired();
            builder.Property(o => o.IsVerified).HasColumnName("IsVerified").IsRequired();

            builder.HasOne(o => o.User);
        }
    }
}
