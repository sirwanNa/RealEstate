using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
	public class PropertyTagConfiguration : IEntityTypeConfiguration<PropertyTag>
	{
		public void Configure(EntityTypeBuilder<PropertyTag> builder)
		{
			builder
				 .HasOne(p => p.PropertyInventory)
				 .WithMany(p => p.PropertyTags)
				 .HasForeignKey(p => p.PropertyInventoryId)
				 .IsRequired(true);

			builder
				.HasOne(p => p.Tag)
				.WithMany(p => p.PropertyTags)
				.HasForeignKey(p => p.TagId)
				.IsRequired(true);

			builder.Property(p => p.Language).IsRequired();

			builder.HasIndex(index => new { index.PropertyInventoryId, index.TagId, index.Language }).IsUnique();
		}
	}
}
