using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
    public class PropertyInventoryTitleConfiguration : IEntityTypeConfiguration<PropertyInventoryTitle>
    {
        public void Configure(EntityTypeBuilder<PropertyInventoryTitle> builder)
        {
            builder
               .HasOne(p => p.PropertyInventory)
               .WithMany(p => p.PropertyInventoryTitles)
               .HasForeignKey(p => p.PropertyInventoryId)
               .IsRequired(true);
           
            builder.Property(p => p.Title).HasMaxLength(500).IsRequired();
            builder.Property(p => p.UrlTitle).HasMaxLength(500).IsRequired();
            builder.Property(p => p.ShortDescription).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Language).IsRequired();
            builder.Property(p => p.PaymentConditions).HasMaxLength(1000).IsRequired(false);

            builder.HasIndex(p => new { p.Title,p.Language }).IsUnique(); 
            builder.HasIndex(p => new { p.PropertyInventoryId, p.Language }).IsUnique();
			builder.HasIndex(p => new { p.UrlTitle, p.Language }).IsUnique();
        }
    }
}
