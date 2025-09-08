using RealEstate.Domain.Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.Blog
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {

            builder.Property(p => p.Title).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.UrlTitle).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Language).IsRequired();

            builder.HasIndex(index => new { index.Title, index.Language }).IsUnique();
            builder.HasIndex(index => new { index.UrlTitle, index.Language }).IsUnique(); 
        }
    }
}
