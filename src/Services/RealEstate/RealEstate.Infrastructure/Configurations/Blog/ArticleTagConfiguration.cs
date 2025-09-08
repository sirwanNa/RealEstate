using RealEstate.Domain.Entities.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Infrastructure.Configurations.Blog
{
    public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder
             .HasOne(p => p.Article)
             .WithMany(p => p.ArticleTags)
             .HasForeignKey(p => p.ArticleId)
             .IsRequired(true);

            builder
                .HasOne(p => p.Tag)
                .WithMany(p => p.ArticleTags)
                .HasForeignKey(p => p.TagId)
                .IsRequired(true);

            builder.Property(p => p.Language).IsRequired();

            builder.HasIndex(index => new { index.ArticleId,index.TagId, index.Language}).IsUnique();
        }
    }
}
