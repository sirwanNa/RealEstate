using RealEstate.Domain.Entities.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.Setting
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(p => p.FileName).HasMaxLength(500).IsRequired();
            builder.Property(p => p.FilePath).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.RelatedId).IsRequired();
            builder.Property(p => p.EntityType).IsRequired();
            builder.HasIndex(index => new { index.RelatedId, index.FileName });

        }
    }
}
