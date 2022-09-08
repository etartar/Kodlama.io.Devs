using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class TechnologyConfigurations : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.ToTable("Technologies");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId").IsRequired();
            builder.Property(t => t.Name).HasColumnName("Name").IsRequired();

            builder.HasOne(t => t.ProgrammingLanguage);
        }
    }
}
