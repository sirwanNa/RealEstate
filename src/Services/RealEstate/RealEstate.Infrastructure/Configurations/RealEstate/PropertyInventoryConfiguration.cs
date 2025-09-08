using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
    public class PropertyInventoryConfiguration : IEntityTypeConfiguration<PropertyInventory>
    {
        public void Configure(EntityTypeBuilder<PropertyInventory> builder)
        {
            builder
               .HasOne(p => p.Region)
               .WithMany(p => p.PropertyInventory_Regions)
               .HasForeignKey(p => p.RegionId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

            builder
               .HasOne(p => p.Builder)
               .WithMany(p => p.PropertyInventory_Builders)
               .HasForeignKey(p => p.BuilderId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(true);

            builder.Property(p => p.StructureType).IsRequired();           
            builder.Property(p => p.Currency).IsRequired();
            builder.Property(p => p.Prepayment).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.DuringProjectPayment).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.HandoverPayment).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.AfterHandoverPayment).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.StartDate).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.FinishDate).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.BrochureLink).HasMaxLength(300).IsRequired(false);

        }
    }
}
