using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
    public class PropertyFeatureConfiguration : IEntityTypeConfiguration<PropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyFeature> builder)
        {
            builder
             .HasOne(p => p.PropertyInventory)
             .WithMany(p => p.PropertyFeatures)
             .HasForeignKey(p => p.PropertyInventoryId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired(true);
            builder.Property(p => p.FeatureType).IsRequired();
        }
    }
}
