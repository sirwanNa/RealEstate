using RealEstate.Domain.Entities.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.Setting
{
    public class ConstantConfiguration : IEntityTypeConfiguration<Constant>
    {
        public void Configure(EntityTypeBuilder<Constant> builder)
        {
            builder.Property(p => p.Type).IsRequired();
        }
    }
}
