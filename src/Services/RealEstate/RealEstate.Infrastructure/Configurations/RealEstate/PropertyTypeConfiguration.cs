using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder
                .HasOne(p => p.PropertyInventory)
                .WithMany(p => p.PropertyTypes)
                .HasForeignKey(p => p.PropertyInventoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
            builder.Property(p => p.RealEstateType).IsRequired();
            builder.HasIndex(p => new { p.PropertyInventoryId, p.RealEstateType }).IsUnique();
        }
    }
}
