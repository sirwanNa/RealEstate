using RealEstate.Domain.Entities.RealEstate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.RealEstate
{
	public class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Message).HasMaxLength(1000).IsRequired();
			builder.Property(p => p.Phone).HasMaxLength(20).IsRequired();
			builder.Property(p => p.Email).HasMaxLength(50).IsRequired(false);
		}
	}
}
