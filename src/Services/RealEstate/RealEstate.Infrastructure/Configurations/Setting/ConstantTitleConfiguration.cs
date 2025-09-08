using RealEstate.Domain.Entities.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.Setting
{
    public class ConstantTitleConfiguration : IEntityTypeConfiguration<ConstantTitle>
    {
        public void Configure(EntityTypeBuilder<ConstantTitle> builder)
        {
            builder
               .HasOne(p => p.Constant)
               .WithMany(p => p.ConstantTitles)
               .HasForeignKey(p => p.ConstantId)
               .IsRequired(true);

            builder.Property(p => p.Title).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(2000).IsRequired(false);
            builder.Property(p => p.Language).IsRequired();

            builder.HasIndex(index => new { index.ConstantId, index.Language });

        }
    }
}
