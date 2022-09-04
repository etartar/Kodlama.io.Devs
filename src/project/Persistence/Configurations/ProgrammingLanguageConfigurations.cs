using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProgrammingLanguageConfigurations : IEntityTypeConfiguration<ProgrammingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
        {
            builder.ToTable("ProgrammingLanguages");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();

            ProgrammingLanguage[] languages = {
                new(1, "C#"),
                new(2, "Java"),
                new(3, "Python")
            };

            builder.HasData(languages);
        }
    }
}
